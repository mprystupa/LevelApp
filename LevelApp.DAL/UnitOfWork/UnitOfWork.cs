using LevelApp.DAL.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Extensions;
using LevelApp.DAL.Context;
using LevelApp.DAL.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace LevelApp.DAL.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private LevelAppContext Context { get; set; }
        private Dictionary<Type, object> Repositories { get; set; }
        private bool _disposed = false;

        public UnitOfWork(LevelAppContext context)
        {
            Context = context;
            Repositories = new Dictionary<Type, object>();
            InitializeRepositories();
        }

        public TRepository GetRepository<TRepository>()
        {
            return (TRepository) Repositories[typeof(TRepository)];
        }

        public void Save(int userId = -1)
        {
            Context.SaveChanges(userId);
        }

        public async Task SaveAsync(int userId = -1)
        {
            await Context.SaveChangesAsync(userId);
        }

        private void Dispose(bool disposing)
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
