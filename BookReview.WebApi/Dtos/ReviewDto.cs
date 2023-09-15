namespace BookReview.WebApi.Dtos;

public record ReviewRequest {
    public int? Id { get; init; }
    public required string ReviewText { get; init; }
    public required double Rating { get; init; }
    public required int BookId { get; init; }
}

public record ReviewResponse(int Id, string ReviewText, double Rating, int BookId);