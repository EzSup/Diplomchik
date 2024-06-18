namespace Restaurant.API.Contracts.Customers
{
    public record CustomerCreateRequest(string Name, Guid UserId, Guid? CartId);
}
