using BookReview.Domain.Entities;
using BookReview.Infrastructure.DataContext;
using BookReview.Infrastructure.Common;

namespace BookReview.Infrastructure.Repositories;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository {
    public AuthorRepository(BookReviewContext context): base(context)
    {
    }
}


public interface IAuthorRepository : IRepository<Author> {}