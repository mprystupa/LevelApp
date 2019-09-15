using LevelApp.DAL.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using LevelApp.DAL.Repositories.Base;

namespace LevelApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext Context { get; set; }
        private Dictionary<Type, object> Repositories { get; set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
            Repositories = new Dictionary<Type, object>();
            InitializeRepositories();
        }

        public TRepository GetRepository<TRepository>()
        {
            return (TRepository) Repositories[typeof(TRepository)];
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

        private void InitializeRepositories()
        {
            Repositories.Add(typeof(IUserRepository), new UserRepository(Context));
        }
    }
}
