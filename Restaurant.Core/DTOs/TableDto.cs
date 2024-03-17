using Restaurant.Core.Dtos.DependingDtos;

namespace Restaurant.Core.Dtos;

public class TableDto
{
    public int Id { get; set; }
    public decimal PriceForHour { get; set; }
    public ICollection<BillNoRelatedDto> Bills { get; set; }
    public bool Free { get; set; }
}