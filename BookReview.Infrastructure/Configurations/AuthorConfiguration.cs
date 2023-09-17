using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookReview.Domain.Entities;

namespace BookReview.Infrastructure.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{

	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property(m => m.Name)
				.HasMaxLength(200)
				.IsRequired();
	}
}

