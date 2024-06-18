using Restaurant.Core.Enums;

namespace Restaurant.Core.Models;

public class Discount
{
    public Guid Id { get; set; }
    public DiscountType DiscountType { get; set; } = DiscountType.Dish;
    public double PecentsAmount { get; set; } = 0;

    public Category? Category { get; set; }
    public Cuisine? Cuisine { get; set; }
    public Dish? Dish { get; set; }
}