using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Models.Base;
using LevelApp.DAL.Models.Base.Interfaces;

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
        public int? CourseId { get; set; }
        public bool? IsFirst { get; set; }
        [MaxLength(1000)]
        public string TagList { get; set; }
        public string IconUrl { get; set; }
        
        public virtual AppUser Author { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<AppUserLesson> AppUserLessons { get; set; }
    }
}