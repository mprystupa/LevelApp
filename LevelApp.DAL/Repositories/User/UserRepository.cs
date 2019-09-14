using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LevelApp.DAL.Entities;
using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Repositories.User
{
    public class UserRepository : BaseRepository<CoreUser, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
