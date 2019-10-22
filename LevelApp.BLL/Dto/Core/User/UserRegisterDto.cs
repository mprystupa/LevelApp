namespace LevelApp.BLL.Dto.Core.User
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarBase64 { get; set; }
    }
}