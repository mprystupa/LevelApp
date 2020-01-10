using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class UpdateLessonOperation : BaseOperation<LessonDto, int>
    {
        private DAL.Models.Core.Lesson _lessonToUpdate;
        
        public override async Task GetData()
        {
            _lessonToUpdate = await Repository<ILessonRepository>().GetDetailAsync(x => x.Id == Parameter.Id);
        }

        public override async Task ExecuteValidated()
        {
            Mapper.Map(Parameter, _lessonToUpdate);

            var result = UnitOfWork.GetRepository<ILessonRepository>().Update(_lessonToUpdate);
            OperationResult = result;
            await UnitOfWork.SaveAsync();
            
            await base.ExecuteValidated();
        }
    }
}