
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookReview.WebApi.Dtos;
using BookReview.Entities.Models;
using BookReview.WebApi.Repositories;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
	private readonly IAuthorRepository authorRepository;
	private IUnitOfWork unitOfWork;
	private IMapper mapper;
	
	public AuthorController(IAuthorRepository authorRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.authorRepository = authorRepository;
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}


	[HttpGet]
	public ActionResult<IEnumerable<AuthorResponse>> GetAll()
	{
		var authors = authorRepository.FindAll();
		if(authors == null)
		{
			return NotFound(new List<AuthorResponse>());
		}
		var authorsDto = mapper.Map<List<AuthorResponse>>(authors);
		return authorsDto;
	}

	[HttpGet("{id}")]
	public ActionResult<AuthorResponse> Get(int id)
	{
		var author = authorRepository.FindById(id);
		if(author == null) 
		{
			return NotFound(new {});
		}

		var authorDto = mapper.Map<AuthorResponse>(author);
		return authorDto;
	}

	[HttpPost]
	public ActionResult<AuthorResponse> Post([FromBody] AuthorRequest author)
	{
		var newAuthor = mapper.Map<Author>(author);
		authorRepository.Add(newAuthor);
		SaveAuthor();

		return Ok(mapper.Map<AuthorResponse>(newAuthor));
	}

	[HttpPut("{id}")]
	public ActionResult<AuthorResponse> Put(int id, [FromBody] AuthorRequest author)
	{
		 if (id != author.Id) 
		 {
			return BadRequest("Mismatch Author Ids");
		 }

		 var existingAuthor = authorRepository.FindById(id);
		 if(existingAuthor == null)
		 {
			return NotFound(new {});
		 }

		 mapper.Map(author, existingAuthor);

		 authorRepository.Update(existingAuthor);
		 SaveAuthor();

		return Ok(mapper.Map<AuthorResponse>(existingAuthor));
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var author =  authorRepository.FindById(id);
		if(author == null) 
		{
			return NotFound(new {});
		}
		
		authorRepository.Remove(author);
		 SaveAuthor();

		return NoContent();
	}


	private void SaveAuthor()
	{
		unitOfWork.Commit();
	}
}

