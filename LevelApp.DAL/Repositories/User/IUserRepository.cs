using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.DAL.Models;
using LevelApp.DAL.Repositories.Base;

namespace LevelApp.DAL.Repositories.User
{
    public interface IUserRepository : IBaseRepository<CoreUser, long>
    {
    }
}
