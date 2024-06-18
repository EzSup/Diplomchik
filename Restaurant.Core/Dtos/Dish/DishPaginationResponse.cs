namespace Restaurant.Core.Dtos.Dish
{
    public record DishPaginationResponse(
        Guid Id,
        string Name,
        string PhotoLink,
        decimal OriginalPrice,
        decimal ResultingPrice,
        double Rate
        );
}
