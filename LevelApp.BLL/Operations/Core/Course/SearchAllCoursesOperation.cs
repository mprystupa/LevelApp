using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Permissions;
using LevelApp.DAL.Repositories.Course;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class SearchAllCoursesOperation : BaseCourseOperation<CourseSearchParametersDto, CourseSearchResultsDto>
    {
        public override async Task ExecuteValidated()
        {
            var results = await Repository<ICourseRepository>()
                .GetPaginatedCoursesAsync(
                    Parameter.CurrentPage, 
                    Parameter.CardsPerPage, 
                    null,
                    lesson => (string.IsNullOrEmpty(Parameter.SearchLessonText) 
                               || lesson.Name.ToLower().Contains(Parameter.SearchLessonText.ToLower())
                               || lesson.Description.ToLower().Contains(Parameter.SearchLessonText.ToLower())),
                    CourseOrderQuery(Parameter));

            OperationResult = new CourseSearchResultsDto()
            {
                SearchResults = MapCourseSearchEntry(results),
                TotalPages = results.TotalPages,
                PageIndex = results.PageIndex
            };
            
            await base.ExecuteValidated();
        }
        
        public override Task AddFrontendPermissions()
        {
            foreach (var searchResult in OperationResult.SearchResults.Where(course => course.AuthorId == CurrentUserId))
            {
                searchResult.Permissions.Add(FrontendPermissions.CanEdit, true);
            }

            return base.AddFrontendPermissions();
        }
    }
}