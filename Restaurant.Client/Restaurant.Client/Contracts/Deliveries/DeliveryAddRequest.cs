namespace Restaurant.Client.Contracts.Deliveries
{
    public class DeliveryAddRequest
    {
        public string? Region { get; set; } = string.Empty;
        public string? SettlementName { get; set; } = string.Empty;
        public string? StreetName { get; set; } = string.Empty;
        public string? StreetNum { get; set; } = string.Empty;
    }
}
