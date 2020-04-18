using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LevelApp.Crosscutting.Services
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string directory, string fileName);
        string GetImageAsDataUri(string imgPath);
    }
}