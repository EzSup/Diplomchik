using Restaurant.Client.Contracts.Enums;

namespace Restaurant.Client.Contracts.Bill
{
    public record BillAddRequest
        (
        Guid CustomerId,
        OrderType Type,
        Guid ReservationOrDeliveryId
        );
}
