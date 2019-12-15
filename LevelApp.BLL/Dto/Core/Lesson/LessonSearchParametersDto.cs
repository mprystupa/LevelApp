using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    public class LessonSearchParametersDto
    {
        public int CurrentPage { get; set; }
        public int CardsPerPage { get; set; }
        public string SearchName { get; set; }
        public string SearchDescription { get; set; }
        public string SearchCategory { get; set; }
        public string SearchLessonText { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public bool IsCreatedByUserOnly { get; set; }
        public LessonStatusEnum? LessonStatus { get; set; }
        public bool? IsFavourite { get; set; }
    }
}