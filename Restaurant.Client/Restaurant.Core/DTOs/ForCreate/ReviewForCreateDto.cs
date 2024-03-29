namespace Restaurant.Core.DTOs;

public class ReviewForCreateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public double Rate { get; set; }
    public int? AuthorId { get; set; }
    public int? DishId { get; set; }
}