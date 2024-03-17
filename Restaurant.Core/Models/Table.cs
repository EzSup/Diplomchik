namespace Restaurant.Core.Models;

public class Table
{
    public int Id { get; set; }
    public decimal PriceForHour { get; set; }
    
    public ICollection<Bill> Bills { get; set; }
    public bool Free { get; set; }
}