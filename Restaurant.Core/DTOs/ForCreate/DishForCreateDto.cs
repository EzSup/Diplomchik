namespace Restaurant.Core.Dtos;

public class DishForCreateDto
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public string? IngredientsList { get; set; }
    public bool Available { get; set; }
    public decimal Price { get; set; }
    public string? PhotoLinks { get; set; }
    public int? DiscountId { get; set; }
    public int? CategoryId { get; set; }
    public int? CuisineId { get; set; }
}