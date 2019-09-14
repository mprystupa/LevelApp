using LevelApp.DAL.Repositories.User;
using System.Threading.Tasks;

namespace LevelApp.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        void Save();
        Task SaveAsync();
        void Dispose();
    }
}
