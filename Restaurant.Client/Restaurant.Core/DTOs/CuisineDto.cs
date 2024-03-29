namespace Restaurant.Core.DTOs;

public class CuisineDto : CustomerForCreateDto
{
    public int Id { get; set; }
    public int? DiscountId { get; set; }
}