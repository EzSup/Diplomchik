using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Core.Models;

public class DishBill
{
    public int DishId { get; set; }
    public int BillId { get; set; }
    
    public int DishesCount { get; set; }
    
    public Dish Dish { get; set; }
    public Bill Bill { get; set; }
}