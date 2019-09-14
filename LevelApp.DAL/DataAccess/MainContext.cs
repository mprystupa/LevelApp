using LevelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.DataAccess
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<CoreUser> CoreUsers { get; set; }
    }
}
