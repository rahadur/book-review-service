namespace BookReview.Entities.Models;

public class Book 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? CoverImage { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }
}