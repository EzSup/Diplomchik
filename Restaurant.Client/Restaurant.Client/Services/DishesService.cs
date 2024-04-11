using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Contracts.Dishes;
using Restaurant.Client.Services.Interfaces;
using System.Drawing.Printing;

namespace Restaurant.Client.Services
{
    public class DishesService : IDishesService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoService _photoService;

        public DishesService(IHttpClientFactory factory, IPhotoService fileUploadService)
        {
            _photoService = fileUploadService;
            _httpClient = factory.CreateClient("API");
        }

        public async Task<string> Add(IEnumerable<IBrowserFile> files, DishCreateRequest request)
        {
            var results = new List<string>();
            if (files != null && files.Count() > 0)
            {
                results = (await _photoService.AddPhotosAsync(files)).Select(x => x.Uri.ToString()).ToList();
            }

            var response = await _httpClient.PostAsJsonAsync($"api/Dishes/Post", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var dish = await GetById(id);
            var imageLinks = dish.PhotoLinks.ToList();
            List<string> photoIds = new();
            if(imageLinks.Count > 0)
            {
                photoIds = imageLinks.Select(x =>  Path.GetFileNameWithoutExtension(x.Split("/")[-1])).ToList();
            }
            foreach (var photoid in photoIds)
            {
                await _photoService.DeletePhotoAsync(photoid);
            }            
            var response = await _httpClient.DeleteAsync($"api/Dishes/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<DishResponse> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Dishes/Get/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var dish = JsonConvert.DeserializeObject<DishResponse>(responseBody);
            return dish;
        }

        public async Task<ICollection<DishResponse>> GetAllDishes()
        {
            var response = await _httpClient.GetAsync($"api/Dishes/GetAll");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var dishes = JsonConvert.DeserializeObject<List<DishResponse>>(responseBody);
            return dishes;
        }

        public async Task<bool> Update(DishRequest request)
        {
            //if(files != null && files.Count() > 0) 
            //{

            //}
            var response = await _httpClient.PutAsJsonAsync($"api/Dishes/Put/{request.Id}", request);
            return response.IsSuccessStatusCode;
        }
    }
}
