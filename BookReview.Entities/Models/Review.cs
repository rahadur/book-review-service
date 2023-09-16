using System.Text.Json.Serialization;

namespace BookReview.Entities.Models;

public class Review 
{
    public int Id { get; set; }
    public double Rating { get; set; }
    public string ReviewText { get; set; } = null!;
    public DateTime CreatedAt { get; set; }


    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    
    public int? UserId { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}