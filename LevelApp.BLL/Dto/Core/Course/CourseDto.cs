using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LevelApp.BLL.Dto.Core.Lesson;

namespace LevelApp.BLL.Dto.Core.Course
{
    public class CourseDto: BaseDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> TagList { get; set; }
        public List<LessonCourseEntryDto> Lessons { get; set; }
        public string TreeData { get; set; }

        public CourseDto()
        {
            TagList = new List<string>();
            Lessons = new List<LessonCourseEntryDto>();
        }
    }
}