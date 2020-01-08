using System.ComponentModel.DataAnnotations;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    public class LessonCourseEntryDto: BaseDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}