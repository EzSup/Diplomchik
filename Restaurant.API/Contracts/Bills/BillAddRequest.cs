using Restaurant.Core.Models;
namespace Restaurant.API.Contracts.Bills
{
    public record BillAddRequest
        (
        Guid CustomerId,
        Bill.OrderType Type,
        Guid ReservationOrDeliveryId
        );    
}
