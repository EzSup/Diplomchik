using Restaurant.Core.Dtos.DependingDtos;

namespace Restaurant.Core.Dtos;

public class ReviewDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public double? Rate { get; set; }
    public int? AuthorId { get; set; }
    public int? DishId { get; set; }
    public DishNoRelatedDto Dish { get; set; }
    public CustomerNoRelatedDto Author { get; set; }
}