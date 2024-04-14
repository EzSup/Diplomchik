using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class DeliveryData
    {
        public Guid Id { get; set; }
        public string? Region { get; set; } = string.Empty;
        public string? SettlementName { get; set; } = string.Empty;
        public string? StreetName { get; set; } = string.Empty;
        public string? StreetNum { get; set; } = string.Empty;

        public Bill? Bill { get; set; }
    }
}
