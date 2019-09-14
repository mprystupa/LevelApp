using LevelApp.DAL.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LevelApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext Context { get; set; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;

            UserRepository = new UserRepository(Context);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
