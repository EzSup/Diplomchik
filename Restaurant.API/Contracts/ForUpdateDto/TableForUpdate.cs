using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.API.Contracts.ForUpdateDto
{
    public class TableForUpdate
    {
        public Guid Id { get; set; }
        public decimal PriceForHour { get; set; }
        public bool Free { get; set; }
    }
}
