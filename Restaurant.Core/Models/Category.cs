using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Core.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public int? DiscountId { get; set; }
    
    public ICollection<Dish> Dishes { get; set; }
    public Discount? Discount { get; set; }
 }