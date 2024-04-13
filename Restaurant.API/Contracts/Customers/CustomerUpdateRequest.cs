namespace Restaurant.API.Contracts.Customers
{
    public record CustomerUpdateRequest(string Name, string PhotoLink, Guid CartId, Guid UserId);
}
