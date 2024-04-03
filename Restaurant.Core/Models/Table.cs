namespace Restaurant.Core.Models;

public class Table
{
    public Guid Id { get; set; }
    public decimal PriceForHour { get; set; }
    public bool Free { get; set; } = true;

    public ICollection<Bill> Bills { get; set; } = [];
    
}