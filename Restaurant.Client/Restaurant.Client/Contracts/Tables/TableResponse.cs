namespace Restaurant.Client.Contracts.Tables
{
    public record TableResponse(
        Guid Id,
        decimal PriceForHour,
        bool Free);
}
