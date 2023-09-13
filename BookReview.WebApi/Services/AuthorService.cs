
using BookReview.Entities.Models;
using BookReview.WebApi.Context;
using Microsoft.EntityFrameworkCore;

namespace BookReview.WebApi.Services;

public class AuthorService
{
    private readonly BookReviewContext dbContext;
    public AuthorService(BookReviewContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Author> GetAll()
    {
        return dbContext.Authors.ToList();
    }

    public Author? FindById(int id)
    {
        return dbContext.Authors.Find(id);
    }


    public void Save(Author author)
    {
        dbContext.Authors.Add(author);
        dbContext.SaveChanges();
    }

    public void Update(Author author)
    {
        dbContext.Authors.Update(author);
        dbContext.Entry(author).State = EntityState.Modified;
        dbContext.SaveChanges();
    }

    public void Delete(Author author)
    {
        dbContext.Authors.Remove(author);
        dbContext.SaveChanges();
    }
}