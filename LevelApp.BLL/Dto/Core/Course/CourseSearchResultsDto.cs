using System.Collections.Generic;

namespace LevelApp.BLL.Dto.Core.Course
{
    public class CourseSearchResultsDto
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<CourseSearchEntryDto> SearchResults { get; set; }
    }
}