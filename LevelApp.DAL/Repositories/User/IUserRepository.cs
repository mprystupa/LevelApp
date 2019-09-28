using LevelApp.DAL.Entities;
using LevelApp.DAL.Repositories.Base;

namespace LevelApp.DAL.Repositories.User
{
    public interface IUserRepository : IBaseRepository<Entities.User, int>
    {
    }
}
