namespace BookReview.Entities.Models;

public class Book 
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string? Description { get; set; }
    public double Rating { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? CoverImage { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}