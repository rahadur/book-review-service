using System.Text;
using System.Globalization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using BookReview.Dtos;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly IConfiguration configuration;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
    }


    [HttpPost(Name = "CreateAccount")]
    public async Task<IActionResult> CreateAccount([FromBody] Registation model)
    {
        var user = new IdentityUser()
        {
            UserName = model.Username,
            Email = model.Email,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            string message = string.Join(", ", errors);

            return BadRequest(new
            {
                Message = message
            });
        }

        return Ok(new
        {
            Message = "Your user account has been successfully created!"
        });

    }


    [HttpPost("Login", Name = "AccountLogin")]
    public async Task<IActionResult> AccountLogin([FromBody] Login login)
    {

        IdentityUser? user;
        if (IsValidEmail(login.Identity))
        {
            user = await userManager.FindByEmailAsync(login.Identity);
        }
        else
        {
            user = await userManager.FindByNameAsync(login.Identity);
        }


        if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
        {
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        return BadRequest(new { Message = "Invalid username, email, or password. Please try again." });

    }


    [HttpPost("Me")]
    [Authorize]
    public IActionResult AccountProfile()
    {
        var user = new
        {
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
            UserName = User.FindFirstValue(ClaimTypes.Name),
            Email = User.FindFirstValue(ClaimTypes.Email)
        };

        return Ok(user);
    }


    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        if (jwtSettings == null)
        {
            throw new InvalidOperationException("JwtSettings section is missing from the configuration.");
        }

        var keyString = jwtSettings.GetValue<string>("Key");
        if (string.IsNullOrEmpty(keyString))
        {
            throw new InvalidOperationException("'Key' is missing from JwtSettings configuration.");
        }

        var key = Encoding.ASCII.GetBytes(keyString);

        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "The user object cannot be null.");
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id ?? throw new InvalidOperationException("User Id is null.")),
                new Claim(ClaimTypes.Name, user.UserName ?? throw new InvalidOperationException("User name is null.")),
                new Claim(ClaimTypes.Email, user.Email ?? throw new InvalidOperationException("User email is null."))
            };

        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(jwtSettings.GetValue<int>("DurationInMinutes")),
            SigningCredentials = creds,
            Issuer = jwtSettings.GetValue<string>("Issuer"),
            Audience = jwtSettings.GetValue<string>("Audience")
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper);
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    private string DomainMapper(Match match)
    {
        // Use IdnMapping class to convert Unicode domain names.
        var idn = new IdnMapping();

        // Pull out and process domain name (throws ArgumentException on invalid)
        var domainName = idn.GetAscii(match.Groups[2].Value);

        return match.Groups[1].Value + domainName;
    }

}