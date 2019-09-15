using LevelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.DataAccess
{
    public class LevelAppContext : DbContext
    {
        public LevelAppContext(DbContextOptions<LevelAppContext> options)
            : base(options)
        {
        }

        public DbSet<CoreUser> CoreUsers { get; set; }
    }
}
