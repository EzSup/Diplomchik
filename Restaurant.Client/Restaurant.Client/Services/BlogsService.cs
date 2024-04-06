using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly HttpClient _httpClient;

        public BlogsService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<string> PostBlogAsync(BlogsCreateRequest blog)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Blogs/Post", blog);
            var result =  await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
