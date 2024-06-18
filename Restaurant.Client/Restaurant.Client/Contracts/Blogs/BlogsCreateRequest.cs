using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Client.Contracts.Blogs
{
    public class BlogsCreateRequest
    {
        [Required]
        public string AuthorName { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public string? ImageLink {  get; set; }
    }
}
