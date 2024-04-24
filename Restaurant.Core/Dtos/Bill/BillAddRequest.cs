using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Dtos.Bill
{
    public record BillAddRequest
        (
        Guid CustomerId,
        Models.Bill.OrderType Type,
        Guid ReservationOrDeliveryId
        );
}
