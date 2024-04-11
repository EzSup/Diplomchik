using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Components.Forms;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IBrowserFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<IEnumerable<ImageUploadResult>> AddPhotosAsync(IEnumerable<IBrowserFile> files);
    }
}
