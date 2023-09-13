
using BookReview.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Entities.Configurations;


public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{

	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property(m => m.Name)
				.HasMaxLength(200)
				.IsRequired();
	}
}

