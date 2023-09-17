
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using BookReview.Domain.Entities;
using BookReview.Infrastructure.Repositories;
using BookReview.WebApi.Dtos;
using BookReview.Infrastructure.Common;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
	private readonly IBookRepository bookRepository;
	private IUnitOfWork unitOfWork;
	private IMapper mapper;

	public BookController(IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.bookRepository = bookRepository;
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}


	[HttpGet(Name = "GetBook")]
	public ActionResult<IEnumerable<BookResponse>> GetAll([FromQuery] FilterQuery query)
	{
		var books = bookRepository.GetPage(query.currentPage, query.pageSize, query.orderBy, query.sort);
		var response = mapper.Map<List<BookResponse>>(books);
		return Ok(response);
	}


	[HttpPost]
	[Authorize]
	public ActionResult<BookResponse> Create([FromBody] BookRequest bookRequest)
	{
		var book = mapper.Map<Book>(bookRequest);

		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrEmpty(userId))
		{
			return Unauthorized(new
			{
				Message = "Unauthorized - You are not allow you create a book"
			});
		}

		book.UserId = userId;
		bookRepository.Add(book);
		SaveBook();

		var response = mapper.Map<BookResponse>(book);
		return Ok(response);
	}


	[HttpGet("{id}")]
	public ActionResult<BookResponse> GetByid(int id)
	{
		var book = bookRepository.FindById(id);
		if (book == null)
		{
			return NotFound(new { });
		}

		var response = mapper.Map<BookResponse>(book);
		return Ok(response);
	}


	[HttpPut("{id}")]
	[Authorize]
	public ActionResult<BookResponse> Update(int id, [FromBody] BookRequest bookReq)
	{
		if (id != bookReq.Id)
		{
			return BadRequest("Mismatch Bookd Ids");
		}

		var book = bookRepository.FindById(id);

		if (book == null)
		{
			return NotFound(new { });
		}

		mapper.Map(bookReq, book);
		bookRepository.Update(book);
		SaveBook();

		var response = mapper.Map<BookResponse>(book);
		return Ok(response);
	}


	[HttpDelete("{id}")]
	public ActionResult<BookResponse> Delete(int id)
	{
		var book = bookRepository.FindById(id);
		if (book == null)
		{
			return NotFound(new { });
		}
		bookRepository.Remove(book);
		SaveBook();

		return NoContent();
	}

	[HttpGet("Author/{id}")]
	[Authorize]
	public ActionResult<IEnumerable<BookResponse>> GetByAuthorId(int id)
	{
		var books = bookRepository.FindMany(book => book.AuthorId == id);
		var response = mapper.Map<List<AuthorResponse>>(books);
		return Ok(response);
	}


	[HttpGet("Gener/{name}")]
	public ActionResult<IEnumerable<BookResponse>> GetByGenerName(string name)
	{
		var books = bookRepository.FindMany(book => book.Genre.ToLower() == name.ToLower());
		var response = mapper.Map<List<BookResponse>>(books);
		return Ok(response);
	}

	private void SaveBook()
	{
		unitOfWork.Commit();
	}
}

