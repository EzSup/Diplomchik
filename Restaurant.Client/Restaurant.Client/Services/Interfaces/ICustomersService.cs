using Restaurant.Client.Contracts.Customers;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<CustomerResponse> GetByUserId(Guid userId);
        Task<bool> Add(CustomerCreateRequest request);
        Task<CustomerResponse> GetFromStorage();
    }
}
