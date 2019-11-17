namespace LevelApp.API.Routes
{
    public class CoreRoutes : BaseRoutes
    {
        
    }

    public class UserRoutes : CoreRoutes
    {
        public const string Login = "login";
        public const string Register = "register";
        public const string CheckEmail = "checkEmail/{email}";
    }

    public class LessonRoutes : CoreRoutes
    {
    }
}