using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Dtos.Delivery
{
    public record DeliveryAddRequest(
        string? Region,
        string? SettlementName,
        string? StreetName,
        string? StreetNum);
}
