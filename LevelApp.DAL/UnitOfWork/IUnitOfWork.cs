using LevelApp.DAL.Repositories.User;
using System.Threading.Tasks;

namespace LevelApp.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>();
        void Save();
        Task SaveAsync();
        void Dispose();
    }
}
