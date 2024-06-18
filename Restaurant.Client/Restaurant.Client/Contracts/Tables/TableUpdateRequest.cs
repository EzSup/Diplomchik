namespace Restaurant.Client.Contracts.Tables
{
    public class TableUpdateRequest
    {
        public Guid Id { get; set; }
        public decimal PriceForHour { get; set; }
        public int Persons { get; set; }
        public int Num { get; set; }
    }
}
