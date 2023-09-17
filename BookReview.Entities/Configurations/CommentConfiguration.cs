
using BookReview.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Entities.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.CommentText)
				.HasMaxLength(500)
				.IsRequired();

		builder.Property(c => c.CreatedAt)
				.IsRequired();

		builder.HasOne(c => c.Review)
				.WithMany(r => r.Comments)
				.HasForeignKey(c => c.ReviewId)
				.IsRequired();

		builder.HasOne(b => b.User)
			.WithMany(u => u.Comments)
			.HasForeignKey(b => b.UserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

