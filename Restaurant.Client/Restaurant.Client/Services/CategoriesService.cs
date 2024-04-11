﻿using Newtonsoft.Json;
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

        public async Task<ICollection<CategoryResponse>> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Categories/GetAll");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryResponse>>(responseBody);
            return categories;
        }
    }
}
