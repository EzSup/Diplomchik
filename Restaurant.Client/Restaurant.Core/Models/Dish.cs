using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Core.Models;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public string? IngredientsList { get; set; }
    public bool Available { get; set; }
    public decimal Price { get; set; }
    public string? PhotoLinks { get; set; }
    
    public int? DiscountId { get; set; }
    public int? CategoryId { get; set; }
    public int? CuisineId { get; set; }
    //navigation
    public Cuisine? Cuisine { get; set; }
    public Discount? Discount { get; set; }
    public Category? Category { get; set; }
    public ICollection<Review> Reviews { get; set; }
    //public ICollection<DishBill> DishBills { get; set; }
    public ICollection<DishCart>? DishCarts { get; set; }
}