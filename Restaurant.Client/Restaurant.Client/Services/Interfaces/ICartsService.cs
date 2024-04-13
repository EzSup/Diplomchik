using Restaurant.Client.Contracts.Carts;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICartsService
    {
        Task<CartResponse> AddDishToCartOfCustomerByCustomerId(Guid userId, Guid dishId, int count);
        Task<CartResponse> Get(Guid cartId);
        Task<CartResponse> GetByCustomer(Guid userId);
        Task<CartResponse> AddDishToCartOfCustomerByUserId(Guid userId, Guid dishId, int count);
    }
}
