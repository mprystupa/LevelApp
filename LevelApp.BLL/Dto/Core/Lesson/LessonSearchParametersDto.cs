using System.Diagnostics.CodeAnalysis;
using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    [ExcludeFromCodeCoverage]
    public class LessonSearchParametersDto : BaseSearchParametersDto
    {
        public bool IsCreatedByUserOnly { get; set; }
        public LessonStatusEnum? LessonStatus { get; set; }
        public bool? IsFavourite { get; set; }
    }
}