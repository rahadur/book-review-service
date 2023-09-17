using BookReview.Domain.Entities;
using BookReview.Infrastructure.DataContext;
using BookReview.Infrastructure.Common;

namespace BookReview.Infrastructure.Repositories;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
	public BookRepository(BookReviewContext context): base(context)
	{
	}
		
}


public interface IBookRepository : IRepository<Book> 
{ 
}

