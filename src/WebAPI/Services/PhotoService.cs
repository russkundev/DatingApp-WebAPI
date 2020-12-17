using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            _cloudinary = new Cloudinary(new Account { 
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret
            });
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var param = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(param);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var result = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var param = new ImageUploadParams { 
                    File = new FileDescription (file.FileName,stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                result = await _cloudinary.UploadAsync(param);
            }

            return result;
        }
    }
}
