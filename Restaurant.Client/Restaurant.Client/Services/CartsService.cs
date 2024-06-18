using Mapster;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Carts;
using Restaurant.Client.Contracts.Customers;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class CartsService : ICartsService
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomersService _customersService;

        public CartsService(IHttpClientFactory factory, ICustomersService customersService)
        {
            _httpClient = factory.CreateClient("API");
            _customersService = customersService;
        }

        public async Task<CartResponse> AddDishToCartOfCustomerByUserId(Guid userId, Guid dishId, int count)
        {
            var customerId = (await _customersService.GetByUserId(userId)).Id;
            return await AddDishToCartOfCustomerByCustomerId(customerId, dishId, count);
        }

        public async Task<CartResponse> AddDishToCartOfCustomerByCustomerId(Guid customerId, Guid dishId, int count)
        {
            var request = new AddDishRequest(customerId, dishId, count);
            var response = await _httpClient.PostAsJsonAsync($"api/Carts/AddDishToCartOfUser",request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CartResponse>(responseBody);
            return result;
        }

        public async Task<CartResponse> Get(Guid cartId)
        {
            var response = await _httpClient.GetAsync($"api/Carts/Get/{cartId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CartResponse>(responseBody);
            return result;
        }

        public async Task<CartResponse> GetByCustomer(Guid customerId)
        {
            var response = await _httpClient.GetAsync($"api/Carts/GetByCustomer/{customerId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CartResponse>(responseBody);
            return result;
        }
    }
}
