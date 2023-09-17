using BookReview.Infrastructure.DataContext;

namespace BookReview.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork 
{
    private BookReviewContext dbContext;
    public UnitOfWork(BookReviewContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Commit()
    {
        dbContext.SaveChanges();
    }
}