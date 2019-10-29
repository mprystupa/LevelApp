using System;
using LevelApp.DAL.Repositories.User;
using System.Threading.Tasks;

namespace LevelApp.DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        TRepository GetRepository<TRepository>();
        void Save(int userId = -1);
        Task SaveAsync(int userId = -1);
    }
}
