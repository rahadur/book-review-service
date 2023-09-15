using System.Linq.Expressions;

namespace BookReview.WebApi.Repositories;


public interface IRepository<TEntity> where TEntity : class 
{

	void Add(TEntity entity);

	void Update(TEntity entity);
	void UpdateRange(IEnumerable<TEntity> entities);

	void Remove(TEntity entity);
	void RemoveRange(IEnumerable<TEntity> entities);
	void RemoveRange(Expression<Func<TEntity, bool>> predicate);

	TEntity? FindById(int id);
	TEntity? FindById(string id);
	IEnumerable<TEntity> FindAll();
	IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate);
	TEntity? Where(Expression<Func<TEntity, bool>> predicate);

	IQueryable<TEntity> Includes(params Expression<Func<TEntity, object>>[] navigationProperties);

	// IPagedList<TEntity> GetPage(Page page, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, bool>> order);
}

