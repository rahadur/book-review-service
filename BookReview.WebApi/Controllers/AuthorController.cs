
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
		var responses = mapper.Map<List<AuthorResponse>>(authors);
		return Ok(responses);
	}


	[HttpGet("{id}")]
	public ActionResult<AuthorResponse> Get(int id)
	{
		var author = authorRepository.FindById(id);
		if (author == null)
		{
			return NotFound(new { });
		}

		var response = mapper.Map<AuthorResponse>(author);
		return Ok(response);
	}

	[HttpPost]
	public ActionResult<AuthorResponse> Post([FromBody] AuthorRequest authorReq)
	{
		var author = mapper.Map<Author>(authorReq);
		authorRepository.Add(author);
		SaveAuthor();

		var response = mapper.Map<AuthorResponse>(author);
		return Ok(response);
	}

	[HttpPut("{id}")]
	public ActionResult<AuthorResponse> Put(int id, [FromBody] AuthorRequest authorReq)
	{
		if (id != authorReq.Id)
		{
			return BadRequest("Mismatch Author Ids");
		}

		var author = authorRepository.FindById(id);
		if (author == null)
		{
			return NotFound(new { });
		}

		mapper.Map(authorReq, author);

		authorRepository.Update(author);
		SaveAuthor();

		var response = mapper.Map<AuthorResponse>(author);
		return Ok(response);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var author = authorRepository.FindById(id);
		if (author == null)
		{
			return NotFound(new { });
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

