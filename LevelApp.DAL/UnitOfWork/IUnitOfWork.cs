using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.DAL.Repositories.User;

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
