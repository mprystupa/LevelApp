using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.DAL.Models;
using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Repositories.User
{
    public class UserRepository: BaseRepository<CoreUser, long>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
