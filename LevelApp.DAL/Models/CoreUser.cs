using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LevelApp.DAL.Models.Base;

namespace LevelApp.DAL.Models
{
    public class CoreUser : Entity<long>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
