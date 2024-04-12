namespace Restaurant.Core.Dtos
{
    public record DishPaginationResponse(
        Guid Id,
        string Name,
        string PhotoLink,
        decimal OriginalPrice,
        decimal ResultingPrice
        );
}
