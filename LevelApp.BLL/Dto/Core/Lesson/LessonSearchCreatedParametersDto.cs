using System.Diagnostics.CodeAnalysis;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    [ExcludeFromCodeCoverage]
    public class LessonSearchCreatedParametersDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}