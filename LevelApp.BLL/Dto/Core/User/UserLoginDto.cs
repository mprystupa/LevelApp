using System.Diagnostics.CodeAnalysis;

namespace LevelApp.BLL.Dto.Core.User
{
    [ExcludeFromCodeCoverage]
    public class UserLoginDto : BaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}