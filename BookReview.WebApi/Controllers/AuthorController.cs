
using Microsoft.AspNetCore.Mvc;
using BookReview.WebApi.Dtos;
using BookReview.Entities.Models;
using BookReview.WebApi.Services;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
	private readonly AuthorService authorService;
	public AuthorsController(AuthorService authorService)
	{
		this.authorService = authorService;
	}


	[HttpGet]
	public IEnumerable<AuthorResponse> Get()
	{
		IList<AuthorResponse> responses = Enumerable.Empty<AuthorResponse>().ToList();
		// TODO refactor with AutoMapper 
		foreach(var aurhor in authorService.GetAll())
		{
			responses.Add(new AuthorResponse(aurhor.Id, aurhor.Name));
		}
		return responses;
	}


	[HttpGet("{id}")]
	public ActionResult<AuthorResponse> Get(int id)
	{
		var author = authorService.FindById(id);
		if (author != null)  {
			return new AuthorResponse(author.Id, author.Name);
		}
		return NotFound(new {});
	}


	[HttpPost]
	public AuthorResponse Post([FromBody] AuthorRequest author)
	{
		var newAuthro = new Author() { Name = author.Name };
		this.authorService.Save(newAuthro);

		return new AuthorResponse(newAuthro.Id, newAuthro.Name );
	}

	[HttpPut("{id}")]
	public ActionResult<AuthorResponse> Put(int id, [FromBody] AuthorRequest author)
	{
		 if (id != author.Id) 
		 {
			return BadRequest("Mismatch Author Ids");
		 }

		 var existingAuthor = authorService.FindById(id);
		 if(existingAuthor == null)
		 {
			return NotFound(new {});
		 }

		 existingAuthor.Name = author.Name;

		 authorService.Update(existingAuthor);

		return new AuthorResponse(existingAuthor.Id, existingAuthor.Name);
	}


	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var author =  authorService.FindById(id);
		if(author == null) 
		{
			return NotFound(new {});
		}
		authorService.Delete(author);
		return NoContent();
	}
}

