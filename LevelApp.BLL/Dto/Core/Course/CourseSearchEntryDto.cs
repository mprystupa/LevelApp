using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Course
{
    public class CourseSearchEntryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public CourseStatusEnum CourseStatus { get; set; }
        public bool IsFavourite { get; set; }
    }
}