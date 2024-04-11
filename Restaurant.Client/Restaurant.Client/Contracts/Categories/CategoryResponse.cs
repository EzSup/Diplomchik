namespace Restaurant.Client.Contracts.Categories
{
    public record CategoryResponse(
        Guid Id,
        string Name,
        Guid? DiscountId);
}
