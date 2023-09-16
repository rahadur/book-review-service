
namespace BookReview.WebApi.Dtos;

public record CommentRequest
{
	public int? Id { get; set; }
	public required string CommentText { get; set; }
	public required int ReviewId { get; set; }
}

public record CommentResponse(int Id, string CommentText, int ReviewId);

