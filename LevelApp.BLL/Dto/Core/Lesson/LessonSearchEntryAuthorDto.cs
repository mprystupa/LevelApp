using System.Diagnostics.CodeAnalysis;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    [ExcludeFromCodeCoverage]
    public class LessonSearchEntryAuthorDto : BaseDto
    {
        public string DisplayName { get; set; }
    }
}