using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookReview.Domain.Entities;

namespace BookReview.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book> 
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.HasKey(m => m.Id);

		builder.Property(b => b.Title)
				.IsRequired();

		builder.Property(b => b.Rating)
			.HasPrecision(3, 2);

		builder.HasMany(e => e.Reviews)
			.WithOne(e => e.Book)
			.HasForeignKey(e => e.BookId)
			.IsRequired();

		builder.HasOne(b => b.User)
			.WithMany(u => u.Books)
			.HasForeignKey(b => b.UserId)
			.OnDelete(DeleteBehavior.Restrict);
		
	}
}