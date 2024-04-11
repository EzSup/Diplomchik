namespace Restaurant.Client.Contracts.Cuisines
{
    public record CuisineRequest(
        Guid Id,
        string Name,
        Guid DiscountId);
}
