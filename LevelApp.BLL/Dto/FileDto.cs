using Microsoft.AspNetCore.Http;

namespace LevelApp.BLL.Dto
{
    public class FileDto : BaseDto
    {
        public IFormFile File { get; set; }
    }
}