namespace Restaurant.API.Contracts.Deliveries
{
    public record DeliveryAddRequest(
        string? Region,
        string? SettlementName,
        string? StreetName,
        string? StreetNum);
}