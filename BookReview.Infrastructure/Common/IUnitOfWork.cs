namespace BookReview.Infrastructure.Common;

public interface IUnitOfWork
{
    void Commit();
}