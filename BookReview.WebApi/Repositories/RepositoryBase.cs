
using BookReview.WebApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookReview.WebApi.Repositories;

public class RepositoryBase<TEntity> : Disposable, IRepository<TEntity> where TEntity : class
{
	protected BookReviewContext dbContext;
	private readonly DbSet<TEntity> dbSet;

	public RepositoryBase(BookReviewContext context)
	{
		dbContext = context;
		dbSet = dbContext.Set<TEntity>();
	}


	public void Add(TEntity entity)
	{
		dbSet.Add(entity);
	}

	public void Update(TEntity entity)
	{
		dbSet.Update(entity);
		dbContext.Entry(entity).State = EntityState.Modified;
	}

	public void UpdateRange(IEnumerable<TEntity> entities)
	{
		dbSet.UpdateRange(entities);
	}

	public void Remove(TEntity entity)
	{
		dbSet.Remove(entity);
	}

	public void RemoveRange(IEnumerable<TEntity> entities)
	{
		dbSet.RemoveRange(entities);
	}

	public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
	{
		IEnumerable<TEntity> entities = dbSet.Where(predicate);
		dbSet.RemoveRange(entities);
		
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


	public IQueryable<TEntity> Includes(params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        IQueryable<TEntity> query = dbSet;

        foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty);
        }

        return query;
    }
}

