using Restaurant.Core.Models;

namespace Restaurant.API.Contracts.Cuisines
{
    public record CuisineUpdateRequest(
        Guid Id,
        string Name,
        Guid DiscountId);
}
