using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class SearchCreatedLessonsOperation : BaseOperation<LessonSearchParametersDto, LessonSearchResultsDto>
    {
        public override async Task ExecuteValidated()
        {
            var results = await Repository<ILessonRepository>()
                .GetPaginatedAsync(
                    Parameter.CurrentPage, 
                    Parameter.CardsPerPage, 
                    lesson => lesson.CreatedBy == CurrentUserId 
                              && (string.IsNullOrEmpty(Parameter.SearchName) || lesson.Name.Contains(Parameter.SearchName))
                              && (string.IsNullOrEmpty(Parameter.SearchDescription)  || lesson.Description.Contains(Parameter.SearchDescription))
                              );
            
            OperationResult = new LessonSearchResultsDto()
            {
                SearchResults = Mapper.Map<List<LessonSearchEntryDto>>(results.ToList()),
                TotalPages = results.TotalPages,
                PageIndex = results.PageIndex
            };
            
            await base.ExecuteValidated();
        }
    }
}