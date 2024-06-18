namespace Restaurant.API.Contracts.Deliveries
{
    public record DeliveryRequest(
        Guid Id,
        string? Region,
        string? SettlementName,
        string? StreetName,
        string? StreetNum
        );
}
