using Restaurant.Client.Contracts.Customers;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<CustomerResponse> GetByUserId(Guid userId);
    }
}
