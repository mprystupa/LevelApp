using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class SearchLessonsOperation : BaseOperation<LessonSearchParametersDto, LessonSearchResultsDto>
    {
        public override async Task ExecuteValidated()
        {
            var results = await Repository<ILessonRepository>().GetPaginatedAsync(Parameter.PageIndex, Parameter.PageSize);
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