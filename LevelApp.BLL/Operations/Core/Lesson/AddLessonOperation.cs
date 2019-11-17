using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class AddLessonOperation : BaseOperation<LessonDto, int>
    {
        public override async Task ExecuteValidated()
        {
            var lesson = Mapper.Map<DAL.Models.Core.Lesson>(Parameter);
            var result = Repository<ILessonRepository>().Insert(lesson);
            OperationResult = result;

            await base.ExecuteValidated();
        }
    }
}