using LevelApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.DataAccess
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CoreUser> CoreUsers { get; set; }
    }
}
