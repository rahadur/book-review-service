using BookReview.Domain.Entities;
using BookReview.Infrastructure.DataContext;
using BookReview.Infrastructure.Common;

namespace BookReview.Infrastructure.Repositories;

public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
{
	public CommentRepository(BookReviewContext context) : base(context)
	{
	}
}


public interface ICommentRepository : IRepository<Comment>
{
}