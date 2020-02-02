using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Models.Base;

namespace LevelApp.DAL.Models.Core
{
    [ExcludeFromCodeCoverage]
    [Table("CoreAppUser")]
    public class AppUser : Entity<int>
    {
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        
        [MaxLength(30)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(50)]
        public string PasswordHash { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string PasswordSalt { get; set; }
        
        public virtual ICollection<AppUserLesson> AppUserLessons { get; set; }
    }
}