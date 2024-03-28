namespace Restaurant.Core.Dtos;

public class CuisineDto : CustomerForCreateDto
{
    public int Id { get; set; }
    public int? DiscountId { get; set; }
}