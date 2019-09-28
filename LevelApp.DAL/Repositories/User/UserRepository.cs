using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Repositories.User
{
    public class UserRepository : BaseRepository<Entities.User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
