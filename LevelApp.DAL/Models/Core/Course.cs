using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Models.Base;

namespace LevelApp.DAL.Models.Core
{
    [ExcludeFromCodeCoverage]
    [Table("CoreCourse")]
    public class Course : Entity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(1000)]
        public string TagList { get; set; }
        public string TreeData { get; set; }
        
        public int AuthorId { get; set; }

        public virtual AppUser Author { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<AppUserCourse> AppUserCourses { get; set; }
    }
}