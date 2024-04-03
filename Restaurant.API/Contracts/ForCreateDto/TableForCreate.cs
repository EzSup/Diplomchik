using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.API.Contracts.ForCreateDto
{
    public class TableForCreate
    {
        public decimal PriceForHour { get; set; }
        public bool Free { get; set; } = true;
    }
}
