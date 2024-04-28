using Newtonsoft.Json;
using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Contracts.Categories;
using Restaurant.Client.Contracts.Cuisines;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient _httpClient;

        public CategoriesService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<string> Add(CategoryCreateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Categories/Post", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<CategoryResponse> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Categories/Get/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<CategoryResponse>(responseBody);
            return categories;
        }

        public async Task<ICollection<CategoryResponse>> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Categories/GetAll");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryResponse>>(responseBody);
            return categories;
        }

        public async Task<bool> Update(CategoryRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Categories/Put/{request.Id}", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Categories/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddDiscount(Guid categoryId, double PercentsAmount)
        {
            var response = await _httpClient.PatchAsync($"api/Categories/AddDiscount?categoryId={categoryId}&PercentsAmount={PercentsAmount}", new StringContent(""));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveDiscount(Guid categoryId)
        {
            var response = await _httpClient.DeleteAsync($"api/Categories/RemoveDiscount/{categoryId}");
            return response.IsSuccessStatusCode;
        }
    }
}
