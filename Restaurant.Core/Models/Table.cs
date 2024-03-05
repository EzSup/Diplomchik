namespace Restaurant.Core.Models;

public class Table
{
    public int Id { get; set; }
    public decimal PriceForHour { get; set; }
    
    public int? BillId { get; set; }
    public Bill Bill { get; set; }
}