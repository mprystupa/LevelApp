using System.Diagnostics.CodeAnalysis;

namespace LevelApp.BLL.Dto.Core.User
{
    [ExcludeFromCodeCoverage]
    public class UserSearchDto : BaseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
