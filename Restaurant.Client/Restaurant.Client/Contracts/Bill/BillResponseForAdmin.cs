using Restaurant.Client.Contracts.Customers;

namespace Restaurant.Client.Contracts.Bill
{
    public class BillResponseForAdmin
    {
        public BillResponse billData { get; set; }
        public CustomerResponse customerData { get; set; }
    }
}
