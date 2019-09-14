using System;
using System.Collections.Generic;
using System.Text;
using LevelApp.DAL.Entities.Base;

namespace LevelApp.DAL.Entities
{
    public class CoreUser : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
