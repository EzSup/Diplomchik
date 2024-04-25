using Newtonsoft.Json;
using Restaurant.Client.Contracts.Categories;
using Restaurant.Client.Contracts.Cuisines;
using Restaurant.Client.Contracts.Dishes;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class CuisinesService : ICuisinesService
    {
        private readonly HttpClient _httpClient;

        public CuisinesService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<string> Add(CuisineCreateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Cuisines/Post", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<ICollection<CuisineResponse>> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Cuisines/GetAll");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var cuisines = JsonConvert.DeserializeObject<List<CuisineResponse>>(responseBody);
            return cuisines;
        }

        public async Task<bool> Update(CuisineRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Cuisines/Put/{request.Id}", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Cuisines/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
