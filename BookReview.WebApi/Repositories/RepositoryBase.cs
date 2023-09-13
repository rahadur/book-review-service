
using Microsoft.EntityFrameworkCore;
using BookReview.WebApi.Context;

namespace BookReview.WebApi.Repositories;

public class RepositoryBase<TEntity> : Disposable, IRepository<TEntity> where TEntity : class
{
	protected DbContext dbContext;
	private readonly DbSet<TEntity> dbSet;

	public RepositoryBase(BookReviewContext context)
	{
		dbContext = context;
		dbSet = dbContext.Set<TEntity>();
	}


	public void Save(TEntity entity)
	{
		dbSet.Add(entity);
	}
}

