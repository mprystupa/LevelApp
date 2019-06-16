using System;
using System.Collections.Generic;
using LevelApp.BLL.Services.Base;
using LevelApp.DAL.Repositories.Base;
using LevelApp.DAL.Repositories.User;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Services.CoreUser
{
    public class UserService : BaseService<DAL.Models.CoreUser, long>, IUserService
    {
        public IBaseRepository<DAL.Models.CoreUser, long> Repository { get; }

        public UserService(IUserRepository repository)
        {
            Repository = repository;
        }
    }
}
