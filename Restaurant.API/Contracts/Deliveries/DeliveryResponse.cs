namespace Restaurant.API.Contracts.Deliveries
{
    public class DeliveryResponse
    {
        public Guid Id { get; set; }
        public string? Region { get; set; } = string.Empty;
        public string? SettlementName { get; set; } = string.Empty;
        public string? StreetName { get; set; } = string.Empty;
        public string? StreetNum { get; set; } = string.Empty;
    }
}
