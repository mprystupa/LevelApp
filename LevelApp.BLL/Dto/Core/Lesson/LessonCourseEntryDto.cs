using System.ComponentModel.DataAnnotations;
using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    public class LessonCourseEntryDto: BaseDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFirst { get; set; }
        public LessonStatusEnum Status { get; set; }
        public string IconUrl { get; set; }
    }
}