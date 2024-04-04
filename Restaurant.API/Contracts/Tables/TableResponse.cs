namespace Restaurant.API.Contracts.Tables
{
    public record TableResponse
    (
        Guid Id,
        decimal PriceForHour,
        bool Free
        );
}
