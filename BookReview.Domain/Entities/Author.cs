namespace BookReview.Domain.Entities;

public class Author 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>().ToList();
}