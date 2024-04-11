namespace Restaurant.API.Contracts.Categories
{
    public record CategoryResponse(
        Guid Id,
        string Name,
        Guid? DiscountId);
}
