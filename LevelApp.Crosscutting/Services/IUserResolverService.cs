namespace LevelApp.Crosscutting.Services
{
    public interface IUserResolverService
    {
        string GetUserEmail();
        TKey GetUserId<TKey>();
    }
}