namespace Restaurant.API.Contracts.Dishes
{
    public record DishPaginationResponse(
        Guid Id,
        string Name,
        string PhotoLink,
        string OriginalPrice,
        string ResultingPrice
        );
}
