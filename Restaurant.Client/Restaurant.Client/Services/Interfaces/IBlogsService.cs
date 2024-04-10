using Microsoft.AspNetCore.Components.Forms;
using Restaurant.Client.Contracts.Blogs;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IBlogsService
    {
        Task<string> PostBlogAsync(IBrowserFile file, BlogsCreateRequest blog);
        Task<IEnumerable<BlogResponse>> GetBlogsByPage(int pageNum, int pageSize);
        Task<BlogResponse> GetBlogById(Guid id);
    }
}
