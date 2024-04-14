namespace Restaurant.API.Contracts.Tables
{
    public record TableResponse
    (
        Guid Id,
        decimal PriceForHour,
        int Persons,
        bool Free
        );
}
