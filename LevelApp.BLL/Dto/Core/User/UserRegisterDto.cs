using System.ComponentModel.DataAnnotations;

namespace LevelApp.BLL.Dto.Core.User
{
    public class UserRegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string AvatarBase64 { get; set; }
    }
}