namespace Restaurant.Core.Dtos.DependingDtos;

public class ReviewNoRelatedDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public double? Rate { get; set; }
    public int? AuthorId { get; set; }
    public int? DishId { get; set; }
}