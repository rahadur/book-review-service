
namespace BookReview.WebApi.Dtos;

public record BookRequest
{
	public int? Id { get; init; }
	public required string Title { get; init; }
	public required string Genre { get; set; }
	public string? Description { get; set; }
	public required DateTime PublicationDate { get; set; }
	public string? CoverImage { get; set; }

	public required int AuthorId { get; set; }
}


public record BookResponse (
	int Id,
	string Title,
	string Gener,
	string Description,
	DateTime PublicationDate,
	int AuthorId,
	string? CoverImage
);



