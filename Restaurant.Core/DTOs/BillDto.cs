using Restaurant.Core.Dtos.DependingDtos;

namespace Restaurant.Core.Dtos;

public class BillDto
{
    public int Id { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime OrderDateAndTime { get; set; }
    public int TipsPercents { get; set; }
    public int? TableId { get; set; }
    public int? CustomerId { get; set; }
    private TableNoRelatedDto? Table { get; set; }
    public CustomerNoRelatedDto? Customer { get; set; }
    public ICollection<DishBillDto> DishBills { get; set; }
}