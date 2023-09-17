using BookReview.Domain.Entities;
using BookReview.Infrastructure.DataContext;
using BookReview.Infrastructure.Common;

namespace BookReview.Infrastructure.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
    public ReviewRepository(BookReviewContext context): base(context)
    {
    }
}


public interface IReviewRepository : IRepository<Review>
{
}