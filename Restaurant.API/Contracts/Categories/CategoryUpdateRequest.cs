using Restaurant.Core.Models;

namespace Restaurant.API.Contracts.Categories
{
    public record CategoryUpdateRequest(
        Guid Id,
        string Name,
        Guid DiscountId);
}
