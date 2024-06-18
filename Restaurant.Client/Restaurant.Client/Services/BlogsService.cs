using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Services.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace Restaurant.Client.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoService _photoService;

        public BlogsService(IHttpClientFactory factory, IPhotoService photoService)
        {
            _photoService = photoService;
            _httpClient = factory.CreateClient("API");
        }

        public async Task<bool> PostBlogAsync(IBrowserFile file, BlogsCreateRequest blog)
        {
            if (file != null)
            {
                //var formFile = await _fileUploadService.ConvertToFormFile(file);
                var uploadResult = await _photoService.AddPhotoAsync(file);
                blog.ImageLink = uploadResult.Uri.ToString();
            }
            var response = await _httpClient.PostAsJsonAsync("api/Blogs/Post", blog);
            var result =  await response.Content.ReadAsStringAsync();
            result = result.Trim('\"');
            Guid value;
            return Guid.TryParse(result,out value);
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

        public async Task<bool> Update(BlogUpdateRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Blogs/Put/{request.Id}", request);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var blog = await GetBlogById(id);
            var response = await _httpClient.DeleteAsync($"api/Blogs/Delete/{id}");            
            if(blog.ImageLink != null)
            {
                await _photoService.DeletePhotoAsync(blog.ImageLink);
            }
            return response.IsSuccessStatusCode;
        }
    }
}
