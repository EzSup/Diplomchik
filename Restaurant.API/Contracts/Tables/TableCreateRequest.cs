namespace Restaurant.API.Contracts.Tables
{
    public record TableCreateRequest(decimal PriceForHour,
        int Persons);
}
