namespace BookReview.WebApi.Repositories;


public interface IRepository<TEntity> where TEntity : class 
{
	void Save(TEntity entity);
}

