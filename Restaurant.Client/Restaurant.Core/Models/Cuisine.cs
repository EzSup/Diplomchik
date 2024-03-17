using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Restaurant.Core.Models;

public class Cuisine
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public int? DiscountId { get; set; }
    
    //navigation
    public Discount? Discount { get; set; }
    public ICollection<Dish> Dishes { get; set; }
}