namespace Restaurant.Client.Contracts.Deliveries
{
    public record DeliveryResponse(
        Guid Id,
        string? Region,
        string? SettlementName,
        string? StreetName,
        string? StreetNum);
}
