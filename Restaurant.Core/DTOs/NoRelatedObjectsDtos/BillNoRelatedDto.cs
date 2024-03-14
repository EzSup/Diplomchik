namespace Restaurant.Core.Dtos.DependingDtos;

public class BillNoRelatedDto
{
    public int Id { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime OrderDateAndTime { get; set; }
    public int TipsPercents { get; set; }
    public int? TableId { get; set; }
    public int? CustomerId { get; set; }
}