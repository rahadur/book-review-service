namespace BookReview.WebApi.Repositories;

public interface IUnitOfWork
{
    void Commit();
}