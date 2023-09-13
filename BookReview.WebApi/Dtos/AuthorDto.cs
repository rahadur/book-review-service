namespace BookReview.WebApi.Dtos;

public record AuthorRequest
{
	public int? Id { get; init; }
	public required string Name { get; init; } = null!;
}


public record AuthorResponse(int Id, string Name);

