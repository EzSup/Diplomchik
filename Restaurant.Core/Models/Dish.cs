using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Core.Models;

public class Dish
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public double Weight { get; set; } = 0;
    public ICollection<string?> IngredientsList { get; set; } = [];
    public bool Available { get; set; } = true;
    public decimal Price { get; set; }
    public ICollection<string?> PhotoLinks { get; set; } = [];

    public Guid? DiscountId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? CuisineId { get; set; }
    //navigation
    public Cuisine? Cuisine { get; set; }
    public Discount? Discount { get; set; }
    public Category? Category { get; set; }
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<DishCart> DishCarts { get; set; } = [];
}