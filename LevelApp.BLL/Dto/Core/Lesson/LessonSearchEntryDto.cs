using System.Diagnostics.CodeAnalysis;
using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    [ExcludeFromCodeCoverage]
    public class LessonSearchEntryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LessonStatusEnum LessonStatus { get; set; }
        public bool IsFavourite { get; set; }
        public LessonSearchEntryCourseDto Course { get; set; }
        public LessonSearchEntryAuthorDto Author { get; set; }
        public string IconUrl { get; set; }
    }
}