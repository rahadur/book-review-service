
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookReview.WebApi.Repositories;

public class RepositoryBase<TEntity> : Disposable, IRepository<TEntity> where TEntity : class
{
	protected DbContext dbContext;
	private readonly DbSet<TEntity> dbSet;

	public RepositoryBase(DbContext context)
	{
		dbContext = context;
		dbSet = dbContext.Set<TEntity>();
	}


	public void Add(TEntity entity)
	{
		dbSet.Add(entity);
		dbContext.SaveChanges();
	}

	public void Update(TEntity entity)
	{
		dbSet.Update(entity);
		dbContext.Entry(entity).State = EntityState.Modified;
		dbContext.SaveChanges();
	}

	public void UpdateRange(IEnumerable<TEntity> entities)
	{
		dbSet.UpdateRange(entities);
		dbContext.SaveChanges();
	}

	public void Remove(TEntity entity)
	{
		dbSet.Remove(entity);
		dbContext.SaveChanges();
	}

	public void RemoveRange(IEnumerable<TEntity> entities)
	{
		dbSet.RemoveRange(entities);
		dbContext.SaveChanges();
	}

	public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
	{
		IEnumerable<TEntity> entities = dbSet.Where(predicate);
		dbSet.RemoveRange(entities);
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

