﻿
using BookReview.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Entities.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
	public void Configure(EntityTypeBuilder<Review> builder)
	{
		builder.HasKey(m => m.Id);

		builder.Property(m => m.ReviewText)
				.IsRequired();

		builder.Property(m => m.Rating)
			.HasPrecision(3, 2)
			.IsRequired();

		builder.HasOne(e => e.Book)
			.WithMany(e =>  e.Reviews)
			.HasForeignKey(e => e.BookId)
			.IsRequired();

		builder.HasMany(r => r.Comments)
				.WithOne(c => c.Review)
				.HasForeignKey(c => c.ReviewId)
				.IsRequired();
	}
}

