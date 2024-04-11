namespace Restaurant.API.Contracts.Cuisines
{
    public record CuisineResponse(
        Guid Id,
        string Name,
        Guid? DiscountId);
}
