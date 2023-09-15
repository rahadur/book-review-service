namespace BookReview.Entities.Models;

public class Comment
{
    public int Id { get; set; }
    public string CommentText { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public int ReviewId { get; set; }
    public Review? Review { get; set; }
}