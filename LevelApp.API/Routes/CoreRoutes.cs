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
        public const string Created = "created";
        public const string Completed = "completed";
        public const string Awaiting = "awaiting";
        public const string Favourite = "favourite";
        public const string Unassigned = "unassigned";
        
        public const string SetIcon = Id + "/" + Icon;
        
        public const string Search = "search";

        public const string PageSize = "{pageSize}";
        public const string PageIndex = "{pageIndex}";

        public const string SearchCreated = Search + "/" + Created;
        public const string SearchCompleted = Search + "/" + Completed;
        public const string SearchAwaiting = Search + "/" + Awaiting;
        public const string SearchFavourite = Search + "/" + Favourite;

        public const string AddFavourite = Id + "/" + Favourite + "/" + Add;
        public const string RemoveFavourite = Id + "/" + Favourite + "/" + Remove;

        public const string Finish = Id + "/finish";
    }

    public class CourseRoutes : CoreRoutes
    {
        public const string Created = "created";
        public const string Completed = "completed";
        public const string Awaiting = "awaiting";
        public const string Favourite = "favourite";
        public const string Attending = "attending";
        
        public const string Search = "search";

        public const string PageSize = "{pageSize}";
        public const string PageIndex = "{pageIndex}";

        public const string SearchCreated = Search + "/" + Created;
        public const string SearchCompleted = Search + "/" + Completed;
        public const string SearchAwaiting = Search + "/" + Awaiting;
        public const string SearchFavourite = Search + "/" + Favourite;

        public const string AddFavourite = Id + "/" + Favourite + "/" + Add;
        public const string RemoveFavourite = Id + "/" + Favourite + "/" + Remove;

        public const string AddAttending = Id + "/" + Attending + "/" + Add;
        public const string RemoveAttending = Id + "/" + Attending + "/" + Remove;
    }
}