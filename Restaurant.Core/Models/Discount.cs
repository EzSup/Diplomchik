namespace Restaurant.Core.Models;

public class Discount
{
    public Guid Id { get; set; }
    public double PecentsAmount { get; set; } = 0;

    public ICollection<Category> Categories { get; set; } = [];
    public Cuisine? Cuisine { get; set; }
    public ICollection<Dish> Dishes { get; set; } = [];
}