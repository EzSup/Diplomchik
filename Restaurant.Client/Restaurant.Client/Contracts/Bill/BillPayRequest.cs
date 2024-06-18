namespace Restaurant.Client.Contracts.Bill
{
    public class BillPayRequest
    {
        public Guid BillId { get; set; }
        public decimal Amount { get; set; }
        public int TipsPercents { get; set; }
    }
}
