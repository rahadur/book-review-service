
using BookReview.Entities.Models;
using BookReview.WebApi.Context;
using Microsoft.EntityFrameworkCore;

namespace BookReview.WebApi.Repositories;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
	public BookRepository(BookReviewContext context): base(context)
	{
	}
		
}


public interface IBookRepository : IRepository<Book> 
{ 
}

