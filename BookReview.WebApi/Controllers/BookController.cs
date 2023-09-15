
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookReview.Entities.Models;
using BookReview.WebApi.Repositories;
using BookReview.WebApi.Dtos;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
	private readonly IBookRepository bookRepository;
	private IUnitOfWork unitOfWork;
	private IMapper mapper;

	public BookController(IBookRepository bookRepository, IUnitOfWork unitOfWork,  IMapper mapper)
	{
		this.bookRepository = bookRepository;
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}


	[HttpGet(Name = "GetBook")]
	public ActionResult<IEnumerable<BookResponse>> GetAll()
	{
		var books = bookRepository.FindAll();
		if (books == null)
		{
			return NotFound(new List<BookResponse>());
		}
		var dto = mapper.Map<List<BookResponse>>(books);
		return dto;
	}

	
	[HttpPost]
	public ActionResult<BookResponse> Create([FromBody] BookRequest book)
	{
		var newBook = mapper.Map<Book>(book);

		bookRepository.Add(newBook);
		SaveBook();

		var bookDto = mapper.Map<BookResponse>(newBook);
		return Ok(bookDto);
	}


	[HttpGet("{id}")]
	public ActionResult<BookResponse> GetByid(int id)
	{
		var book = bookRepository.FindById(id);
		if(book == null)
		{
			return NotFound(new { });
		}

		var bookDto = mapper.Map<BookResponse>(book);
		return bookDto;
	}


	[HttpPut("{id}")]
	public ActionResult<BookResponse> Update(int id, [FromBody] BookRequest book)
	{
		if(id != book.Id)
		{
			return BadRequest("Mismatch Bookd Ids");
		}

		var existingBook = bookRepository.FindById(id);

		if(existingBook == null)
		{
			return NotFound(new { });
		}

		mapper.Map(book, existingBook);
		bookRepository.Update(existingBook);
		SaveBook();

		var bookDto = mapper.Map<BookResponse>(existingBook);

		return bookDto;
	}


	[HttpDelete("{id}")]
	public ActionResult<BookResponse> Delete(int id) 
	{
		var book = bookRepository.FindById(id);
		if(book == null)
		{
			return NotFound(new {});
		}
		bookRepository.Remove(book);
		SaveBook();

		return NoContent();
	}

	[HttpGet("Author/{id}")]
	public ActionResult<IEnumerable<BookResponse>> GetByAuthorId(int id)
	{
		var books = bookRepository.FindMany(book => book.AuthorId == id);
		if (books == null)
		{
			return NotFound(new List<Book>());
		}
		return Ok(books);
	}
	
	
	[HttpGet("Gener/{name}")]
	public ActionResult<IEnumerable<BookResponse>> GetByGenerName(string name)
	{
		var books = bookRepository.FindMany(book => book.Genre.ToLower() == name.ToLower());
		if (books == null)
		{
			return NotFound(new List<Book>());
		}
		var booksDto = mapper.Map<List<BookResponse>>(books);
		return booksDto;
	}

	private void SaveBook()
	{
		unitOfWork.Commit();
	}
}

