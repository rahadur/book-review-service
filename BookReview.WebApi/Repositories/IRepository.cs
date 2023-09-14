using System.Linq.Expressions;

namespace BookReview.WebApi.Repositories;


public interface IRepository<TEntity> where TEntity : class 
{

	void Add(TEntity entity);

	//void Update(TEntity entity);
	//void UpdateRange(IEnumerable<TEntity> entities);

	//void Remove(TEntity entity);
	//void RemoveRange(IEnumerable<TEntity> entities);
	//void RemoveRange(Expression<Func<TEntity, bool>> predicate);

	TEntity? FindById(int id);
	TEntity? FindById(string id);
	TEntity? Where(Expression<Func<TEntity, bool>> predicate);
	IEnumerable<TEntity> FindAll();
	IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate);

	// IPagedList<TEntity> GetPage(Page page, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, bool>> order);
}

