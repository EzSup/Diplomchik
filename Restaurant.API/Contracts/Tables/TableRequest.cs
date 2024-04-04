namespace Restaurant.API.Contracts.Tables
{
    public record TableRequest(
        decimal PriceForHour,
        bool Free);
}
