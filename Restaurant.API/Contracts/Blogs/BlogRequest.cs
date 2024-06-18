using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Contracts.Blogs
{
    public record BlogRequest(
        string AuthorName,
        string Title,
        string Content,
        string? ImageLink);
}
