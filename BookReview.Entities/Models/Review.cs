namespace BookReview.Entities.Models;

public class Review 
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? UserId { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}