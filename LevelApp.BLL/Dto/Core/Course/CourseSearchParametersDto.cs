using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Course
{
    public class CourseSearchParametersDto : BaseSearchParametersDto
    {
        public bool IsCreatedByUserOnly { get; set; }
        public CourseStatusEnum? CourseStatus { get; set; }
        public bool? IsFavourite { get; set; }
    }
}