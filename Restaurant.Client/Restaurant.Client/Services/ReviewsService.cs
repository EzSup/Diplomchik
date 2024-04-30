using Newtonsoft.Json;
using Restaurant.Client.Contracts.Reviews;
using Restaurant.Client.Contracts.Tables;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly HttpClient _httpClient;

        public ReviewsService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<ReviewOfDishResponse>> GetByDishId(Guid dishId, int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/Reviews/GetReviewsOfDish?dishId={dishId}&pageIndex={pageIndex}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ICollection<ReviewOfDishResponse>>(responseBody);
            return result;
        }

        public async Task<bool> Post(ReviewCreateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Reviews/Post", request);
            return response.IsSuccessStatusCode;
        }
    }
}
