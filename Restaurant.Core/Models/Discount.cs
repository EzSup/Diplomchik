namespace Restaurant.Core.Models;

public class Discount
{
    public int Id { get; set; }
    public double PecentsAmount { get; set; }
    
    //navigation
    public ICollection<Category> Categories { get; set; }
    public Cuisine? Cuisine { get; set; }
    public ICollection<Dish> Dishes { get; set; }
}