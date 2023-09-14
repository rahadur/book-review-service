using BookReview.Entities.Models;
using BookReview.WebApi.Context;

namespace BookReview.WebApi.Repositories;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository {
    public AuthorRepository(BookReviewContext context): base(context)
    {
    }
}


public interface IAuthorRepository : IRepository<Author> {}