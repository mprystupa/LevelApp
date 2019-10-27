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
        private IHttpContextAccessor HttpContextAccessor { get; set; }
        private Func<int> _requestUserIdFunc;
        private bool _disposed = false;

        public UnitOfWork(LevelAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Repositories = new Dictionary<Type, object>();
            InitializeRepositories();

            HttpContextAccessor = httpContextAccessor;
            _requestUserIdFunc = () => HttpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();
        }

        public TRepository GetRepository<TRepository>()
        {
            return (TRepository) Repositories[typeof(TRepository)];
        }

        public void Save()
        {
            Context.SaveChanges(_requestUserIdFunc.Invoke());
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync(_requestUserIdFunc.Invoke());
        }

        /// <summary>
        /// Sets function that returns currently logged user ID value. Used only in unit testing.
        /// </summary>
        /// <param name="requestUserIdFunc"></param>
        public void SetRequestUserIdFunction(Func<int> requestUserIdFunc)
        {
            _requestUserIdFunc = requestUserIdFunc;
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
