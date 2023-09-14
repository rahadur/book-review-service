
using Microsoft.EntityFrameworkCore;
using BookReview.WebApi.Context;
using System.Linq.Expressions;

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


	public void Add(TEntity entity)
	{
		dbSet.Add(entity);
		dbContext.SaveChanges();
	}

	public TEntity? FindById(int id)
	{
		return dbSet.Find(id);
	}

	public TEntity? FindById(string id)
	{
		return dbSet.Find(id);
	}

	public IEnumerable<TEntity> FindAll()
	{
		return dbSet.ToList();
	}

	public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate)
	{
		return dbSet.Where(predicate).ToList();
	}

	public TEntity? Where(Expression<Func<TEntity, bool>> predicate)
	{
		return dbSet.Where(predicate).FirstOrDefault();
	}
}

