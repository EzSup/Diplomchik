using Restaurant.Core.Models;
namespace Restaurant.API.Contracts.Bills
{
    public record BillAddRequest
        (decimal PaidAmount,
        int TipsPercents,
        Guid CustomerId,
        Bill.OrderType Type,
        Guid ReservationOrDeliveryId
        );    
}
