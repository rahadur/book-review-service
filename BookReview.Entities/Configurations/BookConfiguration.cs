
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookReview.Entities.Models;

namespace BookReview.Entities.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book> 
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.Property(b => b.Title)
				.IsRequired();
	}
}