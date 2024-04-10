using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Services.Interfaces;
using System.Text.Json;

namespace Restaurant.Client.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoService _fileUploadService;

        public BlogsService(IHttpClientFactory factory, IPhotoService fileUploadService)
        {
            _fileUploadService = fileUploadService;
            _httpClient = factory.CreateClient("API");
        }

        public async Task<string> PostBlogAsync(IBrowserFile file, BlogsCreateRequest blog)
        {
            if (file != null)
            {
                //var formFile = await _fileUploadService.ConvertToFormFile(file);
                var uploadResult = await _fileUploadService.AddPhotoAsync(file);
                blog.ImageLink = uploadResult.Uri.ToString();
            }
            var response = await _httpClient.PostAsJsonAsync("api/Blogs/Post", blog);
            var result =  await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<IEnumerable<BlogResponse>> GetBlogsByPage(int pageNum, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/Blogs/GetByPage?page={pageNum}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var blogs = JsonConvert.DeserializeObject<List<BlogResponse>>(responseBody);
            return blogs;
        }

        public async Task<BlogResponse> GetBlogById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Blogs/Get/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<BlogResponse>(responseBody);
            return blog;
        }
    }
}
