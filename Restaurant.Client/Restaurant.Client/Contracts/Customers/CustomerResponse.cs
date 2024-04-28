namespace Restaurant.Client.Contracts.Customers
{
    public record CustomerResponse(
        Guid Id, string Name, string PhoneNum, string Email
        );
}
