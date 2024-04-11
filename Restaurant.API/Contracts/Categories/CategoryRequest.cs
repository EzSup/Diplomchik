using Restaurant.Core.Models;

namespace Restaurant.API.Contracts.Categories
{
    public record CategoryRequest(
        Guid Id,
        string Name,
        Guid DiscountId);
}
