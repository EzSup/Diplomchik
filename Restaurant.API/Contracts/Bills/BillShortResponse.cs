using static Restaurant.Core.Models.Bill;

namespace Restaurant.API.Contracts.Bills
{
    public record BillShortResponse
        (Guid Id, decimal TotalPrice, DateTime OrderDateAndTime,
        decimal Tips, OrderType Type, bool IsPaid);
}
