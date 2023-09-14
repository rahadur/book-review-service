
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookReview.Entities.Models;
using BookReview.WebApi.Repositories;
using BookReview.WebApi.Dtos;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
	private readonly IBookRepository bookRepository;
	private IMapper mapper;

	public BookController(IBookRepository bookRepository, IMapper mapper)
	{
		this.mapper = mapper;
		this.bookRepository = bookRepository;
	}


	[HttpGet(Name = "GetBook")]
	public ActionResult<IEnumerable<BookResponse>> Get()
	{
		var books = bookRepository.FindAll();
		if (books == null)
		{
			return NotFound(new List<BookResponse>());
		}
		var dto = mapper.Map<List<BookResponse>>(books);
		return dto;
	}


	[HttpGet("{id}")]
	public ActionResult<BookResponse> GetByid(int id)
	{
		var book = bookRepository.FindById(id);
		if(book == null)
		{
			return NotFound(new { });
		}

		BookResponse bookDto = mapper.Map<BookResponse>(book);
		return bookDto;
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
	public ActionResult<IEnumerable<BookResponse>> GetByGenerId(string name)
	{
		var books = bookRepository.FindMany(book => book.Genre.ToLower() == name.ToLower());
		if (books == null)
		{
			return NotFound(new List<Book>());
		}
		var booksDto = mapper.Map<List<BookResponse>>(books);
		return booksDto;
	}



	[HttpPost]
	public ActionResult<BookResponse> Create([FromBody] BookRequest book)
	{
		var newBook = mapper.Map<Book>(book);

		bookRepository.Add(newBook);
		var bookDto = mapper.Map<BookResponse>(newBook);
		return Ok(bookDto);
	}
}

