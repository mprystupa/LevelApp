using LevelApp.Crosscutting.Extensions;
using Microsoft.AspNetCore.Http;

namespace LevelApp.Crosscutting.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserResolverService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.GetLoggedInUserEmail();
        }

        public TKey GetUserId<TKey>()
        {
            return _httpContextAccessor.HttpContext.User.GetLoggedInUserId<TKey>();
        }
    }
}