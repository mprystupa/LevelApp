using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace LevelApp.Crosscutting.Services
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _environment;
        
        public FileService(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        
        public async Task<string> SaveFile(IFormFile file, string directory, string fileName)
        {
            var uploadDirectory = Path.Combine(_environment.WebRootPath, directory);
            var resultDirectory = string.Empty;
            var relativeDirectory = string.Empty;

            Directory.CreateDirectory(uploadDirectory);

            if (file.Length <= 0) return resultDirectory;
            
            var fullFileName = fileName + Path.GetExtension(file.FileName);
            resultDirectory = Path.Combine(uploadDirectory, fullFileName);
            relativeDirectory = Path.Combine(directory, fullFileName);
            using (var fileStream = new FileStream(resultDirectory, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return relativeDirectory;
        }
        
        public string GetImageAsDataUri(string imgPath)
        {
            var serverpath = Path.Combine(_environment.WebRootPath, imgPath);
            var contents = File.ReadAllBytes(serverpath);
            return $"data:image/png;base64,{Convert.ToBase64String(contents)}";
        }
    }
}