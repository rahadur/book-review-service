using BookReview.WebApi.Context;
using Microsoft.EntityFrameworkCore;

namespace BookReview.WebApi.Repositories;

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