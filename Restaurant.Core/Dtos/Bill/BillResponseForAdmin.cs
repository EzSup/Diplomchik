using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Core.Models.Bill;
using Restaurant.Core.Dtos.Customer;

namespace Restaurant.Core.Dtos.Bill
{
    public class BillResponseForAdmin
    {
        public BillResponse billData { get; set; }
        public CustomerResponse customerData { get; set; }
    }
}
