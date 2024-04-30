namespace Restaurant.Core.Models;

public class Review
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Content { get; set; } = string.Empty;
    public double Rate { get; set; } = 0;
    public DateTime Posted { get; private set; }

    public Review()
    {
        Posted = DateTime.UtcNow;
    }

    public Guid AuthorId { get; set; }
    public Guid DishId { get; set; }

    public Dish? Dish { get; set; }
    public Customer? Author { get; set; }
}