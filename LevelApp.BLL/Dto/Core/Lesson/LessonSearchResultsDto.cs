using System.Collections.Generic;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    public class LessonSearchResultsDto
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<LessonSearchEntryDto> SearchResults { get; set; }
    }
}