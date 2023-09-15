using BookReview.Entities.Models;
using BookReview.WebApi.Context;

namespace BookReview.WebApi.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
    public ReviewRepository(BookReviewContext context): base(context)
    {
    }
}


public interface IReviewRepository : IRepository<Review>
{
}