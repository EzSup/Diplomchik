namespace Restaurant.Client.Contracts.Tables
{
    public class TableCreateRequest
    {        
        public decimal PriceForHour { get; set; }
        public int Persons { get; set; }
    }
}
