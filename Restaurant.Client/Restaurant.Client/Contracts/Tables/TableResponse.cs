namespace Restaurant.Client.Contracts.Tables
{
    public record TableResponse(
        Guid Id,
        decimal PriceForHour,
        int Persons,
        bool Free,
        int Num);
}
