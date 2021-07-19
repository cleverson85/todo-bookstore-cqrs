using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ToDo.Domain.Interfaces
{
    public interface IMinioService
    {
        Task<byte[]> DownloadFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
        Task<bool> UploadFileAsync(IFormFile file);
        Task<bool> FileExistsAsync();
    }
}
