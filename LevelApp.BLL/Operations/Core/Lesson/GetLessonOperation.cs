using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class GetLessonOperation : BaseOperation<int, LessonDto>
    {
        public override async Task ExecuteValidated()
        {
            var lesson = await Repository<ILessonRepository>().GetDetailAsync(x => x.Id == Parameter);
            OperationResult = Mapper.Map<LessonDto>(lesson);
            
            await base.ExecuteValidated();
        }
    }
}