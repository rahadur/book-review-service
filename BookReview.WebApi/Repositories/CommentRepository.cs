using System;
using BookReview.Entities.Models;
using BookReview.WebApi.Context;

namespace BookReview.WebApi.Repositories;

public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
{
	public CommentRepository(BookReviewContext context) : base(context)
	{
	}
}


public interface ICommentRepository : IRepository<Comment>
{
}