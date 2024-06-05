using Newtonsoft.Json;
using Restaurant.Client.Contracts.Reviews;
using Restaurant.Client.Contracts.Tables;
using Restaurant.Client.Services.Interfaces;
using System.Drawing.Printing;

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

        public async Task<(bool ifHas, ReviewOfDishResponse? obj)> HasOwnReview(Guid dishId)
        {
            var response = await _httpClient.GetAsync($"api/Reviews/GetReviewOfUser/{dishId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return (false, null);
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReviewOfDishResponse>(responseBody);
            return (true, result);
        }

        public async Task<ReviewOfDishResponse?> GetOwnReviewOnDish(Guid dishId)
        {
            var response = await _httpClient.GetAsync($"api/Reviews/GetReviewOfUser/{dishId}");
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReviewOfDishResponse>(responseBody);
            return result;
        }
    }
}
