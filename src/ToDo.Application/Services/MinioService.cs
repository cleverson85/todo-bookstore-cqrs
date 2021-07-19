using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio;
using System.IO;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Settings;

namespace ToDo.Application.Services
{
    public class MinioService : IMinioService
    {
        private readonly MinioClient _minioClient;
        private readonly ILogger<MinioService> _logger;
        private readonly MinioSettings _minioSettings;

        public MinioService(MinioClient minioClient, AppSettings appSettings, ILogger<MinioService> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
            _minioSettings = appSettings.MinioSettings;
        }

        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            await CreateBucket();

            using (MemoryStream memoryStream = new())
            {
                file.OpenReadStream().CopyTo(memoryStream);
                await _minioClient.PutObjectAsync(_minioSettings.Bucket, $"bookStore.jpg", memoryStream, memoryStream.Length, "image/jpg");
            }
                
            _logger.LogInformation($"File {_minioSettings.Bucket} created!");
            return true;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            await _minioClient.RemoveObjectAsync(_minioSettings.Bucket, fileName);
            return true;
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            byte[] file = null;

            await _minioClient.GetObjectAsync(_minioSettings.Bucket, fileName, c =>
            {
                using MemoryStream ms = new();
                c.CopyTo(ms);
                file = ms.ToArray();
            });

            return file;
        }

        public async Task<bool> FileExistsAsync()
        {
            var result = await _minioClient.StatObjectAsync(_minioSettings.Bucket, _minioSettings.AccessKey);
            return result != null;
        }

        private async Task CreateBucket()
        {
            var bucketExists = await _minioClient.BucketExistsAsync(_minioSettings.Bucket);

            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(_minioSettings.Bucket);
                _logger.LogInformation($"Bucket {_minioSettings.Bucket} created!");
            }
        }
    }
}
