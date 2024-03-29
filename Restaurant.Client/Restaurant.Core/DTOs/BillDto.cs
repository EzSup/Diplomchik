namespace Restaurant.Core.DTOs;

public class BillDto : BillForCreateDto
{
    public int Id { get; set; }
    public DateTime OrderDateAndTime { get; set; }
    public ICollection<DishBillDto> DishBills { get; set; }
}