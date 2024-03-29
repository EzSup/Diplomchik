using Microsoft.AspNetCore.Components.Forms;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<(int, string)> UploadFileAsync(IBrowserFile file, int maxFileSize, string[] allowedExtensions);
    }
}
