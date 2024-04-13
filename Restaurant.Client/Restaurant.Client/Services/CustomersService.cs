using Mapster;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Contracts.Customers;
using Restaurant.Client.Services.Interfaces;
using System.Text.Json;

namespace Restaurant.Client.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly HttpClient _httpClient;

        public CustomersService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<CustomerResponse> GetByUserId(Guid userId)
        {
            var response = await _httpClient.GetAsync($"/api/Customers/GetByUser/{userId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CustomerResponse>(responseBody);
            return result;
        }

        public async Task<bool> Add(CustomerCreateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Customers/Post", request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guid>(responseBody);
            return result != Guid.Empty;
        }
    }
}
