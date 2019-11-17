using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;
using LevelApp.DAL.Repositories.User;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class GetAllLessonsOperation : BaseOperation<int, List<LessonDto>>
    {
        public override async Task ExecuteValidated()
        {
            var allLessons = await UnitOfWork.GetRepository<ILessonRepository>().GetAllAsync();
            OperationResult = Mapper.Map<List<LessonDto>>(allLessons);
            await base.ExecuteValidated();
        }
    }
}