using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public Guid? DiscountId { get; set; }

    public ICollection<Dish> Dishes { get; set; } = [];
    public Discount? Discount { get; set; }
 }