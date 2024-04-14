namespace Restaurant.API.Contracts.Tables
{
    public record TableRequest(
        decimal PriceForHour,
        int Persons);
}
