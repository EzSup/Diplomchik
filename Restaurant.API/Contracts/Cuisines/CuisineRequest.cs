using Restaurant.Core.Models;

namespace Restaurant.API.Contracts.Cuisines
{
    public record CuisineRequest(
        Guid Id,
        string Name,
        Guid DiscountId);
}
