﻿using RochaBlogs.Services.Interfaces;

namespace RochaBlogs.Services
{
    public class DefaultImageService : IImageService
    {
        public string ContentType(IFormFile file)
        {
            return file.ContentType;
        }

        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null)
            {
                return null;
            }

            return $"data:image/{type};base64,{Convert.ToBase64String(data)}";
        }

        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var memoryString = new MemoryStream();
            await file.CopyToAsync(memoryString);
            return memoryString.ToArray();
        }

        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{fileName}";
            return await File.ReadAllBytesAsync(file);
        }

        public int Size(IFormFile file)
        {
            return Convert.ToInt32(file?.Length);
        }
    }
}
