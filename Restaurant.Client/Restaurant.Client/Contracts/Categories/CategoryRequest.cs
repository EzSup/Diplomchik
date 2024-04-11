namespace Restaurant.Client.Contracts.Categories
{
    public record CategoryRequest(
        Guid Id,
        string Name,
        Guid DiscountId);
}
