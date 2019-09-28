using System;
using System.Collections.Generic;

namespace LevelApp.DAL.Entities
{
    public partial class User
    {
        public uint Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
