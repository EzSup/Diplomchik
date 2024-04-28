namespace Restaurant.Client.Contracts.Cuisines
{
    public record CuisineResponse(
        Guid Id,
        string Name,
        double DiscountPercents);
}
