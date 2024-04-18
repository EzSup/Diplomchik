using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Core.Models.Bill;

namespace Restaurant.Core.Dtos
{
    public class BillResponse
    {
        public Guid Id { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime OrderDateAndTime { get; set; }
        public decimal Tips { get; set; } = 0;
        public OrderType Type { get; set; }
        public bool IsPaid { get; set; } = false;

        public Guid? ReservationOrDeliveryId { get; set; }
        public ICollection<DishOfBill> Dishes { get; set; } = [];
    }

    public record DishOfBill(string Name, int Count, decimal Price, decimal SumPrice);
}
