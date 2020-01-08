using System.Collections.Generic;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Permissions;
using LevelApp.DAL.Repositories.Course;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class SearchCreatedCoursesOperation : BaseOperation<CourseSearchParametersDto, CourseSearchResultsDto>
    {
        public override async Task ExecuteValidated()
        {
            var results = await Repository<ICourseRepository>()
                .GetPaginatedAsync(
                    Parameter.CurrentPage, 
                    Parameter.CardsPerPage, 
                    course => course.CreatedBy == CurrentUserId 
                              && (string.IsNullOrEmpty(Parameter.SearchName) || course.Name.Contains(Parameter.SearchName))
                              && (string.IsNullOrEmpty(Parameter.SearchDescription)  || course.Description.Contains(Parameter.SearchDescription))
                );
            
            OperationResult = new CourseSearchResultsDto()
            {
                SearchResults = Mapper.Map<List<CourseSearchEntryDto>>(results),
                TotalPages = results.TotalPages,
                PageIndex = results.PageIndex
            };
            
            await base.ExecuteValidated();
        }

        public override Task AddFrontendPermissions()
        {
            foreach (var operationResultSearchResult in OperationResult.SearchResults)
            {
                operationResultSearchResult.Permissions.Add(FrontendPermissions.CanEdit, true);
            }

            return base.AddFrontendPermissions();
        }
    }
}