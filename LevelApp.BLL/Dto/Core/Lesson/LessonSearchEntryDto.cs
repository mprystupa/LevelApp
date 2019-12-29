using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    public class LessonSearchEntryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public LessonStatusEnum LessonStatus { get; set; }
        public bool IsFavourite { get; set; }
    }
}