using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class SearchCreatedLessonsOperation : BaseLessonOperation<LessonSearchParametersDto, LessonSearchResultsDto>
    {
        public override async Task ExecuteValidated()
        {
            var results = await Repository<ILessonRepository>()
                .GetPaginatedLessonsAsync(
                    Parameter.CurrentPage, 
                    Parameter.CardsPerPage,
                    null,
                    lesson => lesson.CreatedBy == CurrentUserId
                              && (string.IsNullOrEmpty(Parameter.SearchName) || lesson.Name.Contains(Parameter.SearchName))
                              && (string.IsNullOrEmpty(Parameter.SearchDescription)  || lesson.Description.Contains(Parameter.SearchDescription)),
                    LessonOrderQuery(Parameter));
            
            OperationResult = new LessonSearchResultsDto()
            {
                SearchResults = MapLessonSearchEntry(results),
                TotalPages = results.TotalPages,
                PageIndex = results.PageIndex
            };
            
            await base.ExecuteValidated();
        }

        public override Task AddFrontendPermissions()
        {
            OperationResult.SearchResults = AddLessonsFrontendPermissions(OperationResult.SearchResults);

            return base.AddFrontendPermissions();
        }
    }
}