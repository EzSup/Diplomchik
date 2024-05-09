using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Components.Forms;
using Restaurant.Client.Services.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Options;
using Restaurant.Client.Contracts;

namespace Restaurant.Client.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly ICloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);
            _cloudinary = new CloudinaryDotNet.Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IBrowserFile file)
        {
            var name = Guid.NewGuid().ToString();
            name = string.Concat(name, Path.GetExtension(file.Name));
            var uploadResult = new ImageUploadResult();
            if (file.Size > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadResult;
        }

        public async Task<IEnumerable<ImageUploadResult>> AddPhotosAsync(IEnumerable<IBrowserFile> files)
        {
            List<ImageUploadResult> results = new List<ImageUploadResult>();

            foreach(var file in files)
            {
                var uploadResult = await AddPhotoAsync(file);
                results.Add(uploadResult);
            }

            return results;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string imageLink)
        {
            // Розділити рядок за останнім входженням символу '/'
            var lastSlashIndex = imageLink.LastIndexOf('/');
            var publicId = imageLink.Substring(lastSlashIndex + 1);

            // Викликати метод видалення з використанням publicId
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

    }
}
