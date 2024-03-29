namespace Restaurant.Core.DTOs;

public class BillForCreateDto
{
    public decimal PaidAmount { get; set; }
    public int TipsPercents { get; set; }
    public int? TableId { get; set; }
    public int? CustomerId { get; set; }
    
    public IDictionary<string, int> DishesAndCount { get; set; }
}