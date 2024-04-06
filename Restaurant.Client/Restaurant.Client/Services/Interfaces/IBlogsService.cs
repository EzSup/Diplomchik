using Restaurant.Client.Contracts.Blogs;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IBlogsService
    {
        Task<string> PostBlogAsync(BlogsCreateRequest blog);
    }
}
