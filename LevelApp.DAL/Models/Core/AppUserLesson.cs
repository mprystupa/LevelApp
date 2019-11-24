using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.DAL.Models.Base;

namespace LevelApp.DAL.Models.Core
{
    [ExcludeFromCodeCoverage]
    [Table("CoreAppUserLesson")]
    public class AppUserLesson : Entity<int>
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int LessonId { get; set; }
        
        [Required]
        public LessonStatusEnum Status { get; set; }
        [Required]
        public bool IsFavourite { get; set; }
        
        public virtual AppUser User { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}