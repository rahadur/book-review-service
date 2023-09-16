using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookReview.Dtos;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;


    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
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

            return BadRequest(new {
                Message = message
            });
        }

        return Ok(new {
            Message = "Your user account has been successfully created!"
        });
        
    }


    [HttpPost("Login", Name = "AccountLogin")]
    public async Task<IActionResult> AccountLogin()
    {
        return Ok();
    }


    [HttpPost("Me")]
    public async Task<IActionResult> AccountProfile()
    {
        return Ok();
    }

}