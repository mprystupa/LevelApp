using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Models.Base;

namespace LevelApp.DAL.Models.Core
{
    [ExcludeFromCodeCoverage]
    [Table("CoreLesson")]
    public class Lesson : Entity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        
        public virtual AppUser Author { get; set; }
        public virtual ICollection<AppUserLesson> AppUserLessons { get; set; }
    }
}