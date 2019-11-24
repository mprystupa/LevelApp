using System.Diagnostics.CodeAnalysis;
using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    [ExcludeFromCodeCoverage]
    public class LessonSearchAttendingParametersDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public LessonStatusEnum? LessonStatus { get; set; }
        public bool? IsFavourite { get; set; }
    }
}