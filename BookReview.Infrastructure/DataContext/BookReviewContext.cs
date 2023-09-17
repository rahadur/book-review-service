using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using BookReview.Domain.Entities;
using BookReview.Infrastructure.Configurations;

namespace BookReview.Infrastructure.DataContext;

public class BookReviewContext : IdentityDbContext<IdentityUser>
{
    public BookReviewContext(DbContextOptions<BookReviewContext> options) : base(options) 
    {
    }

    public DbSet<Book>  Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
		new BookConfiguration().Configure(mb.Entity<Book>());
        new AuthorConfiguration().Configure(mb.Entity<Author>());
        new ReviewConfiguration().Configure(mb.Entity<Review>());
		new CommentConfiguration().Configure(mb.Entity<Comment>());
        
        base.OnModelCreating(mb);
    }
}