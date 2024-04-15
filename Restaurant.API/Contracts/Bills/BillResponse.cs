using static Restaurant.Core.Models.Bill;

namespace Restaurant.API.Contracts.Bills
{
    public class BillResponse
    {
        public Guid Id { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal PaidAmount { get; set; } = 0;
        public DateTime OrderDateAndTime { get; set; }
        public int TipsPercents { get; set; } = 0;
        public OrderType Type { get; set; }

        public Guid CustomerId { get; set; }
        public Guid CartId { get; set; }
        public Guid? ReservationId { get; set; }
        public Guid? DeliveryId { get; set; }
    }
}
