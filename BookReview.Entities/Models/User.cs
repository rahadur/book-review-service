using Microsoft.AspNetCore.Identity;

namespace BookReview.Entities.Models;

public class User : IdentityUser
{
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}