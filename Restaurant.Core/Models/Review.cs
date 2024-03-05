namespace Restaurant.Core.Models;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public double? Rate { get; set; }
    public int? AuthorId { get; set; }
    public int? DishId { get; set; }

    public Dish Dish { get; set; }
    public Customer Author { get; set; }
}