namespace Restaurant.Client.Contracts.Tables
{
    public class ReserveRequest
    {
        public Guid tableId { get; set; }
        public DateTime start { get; set; }
    }
}
