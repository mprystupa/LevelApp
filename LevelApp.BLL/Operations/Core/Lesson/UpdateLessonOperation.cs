using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class UpdateLessonOperation : BaseOperation<LessonDto, int>
    {
        public override async Task Validate()
        {
            if (!Repository<ILessonRepository>().CheckIfExists(x => x.Id == Parameter.Id))
            {
                Errors.Add("Lesson does not exist.", HttpStatusCode.NotFound);
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            var lesson = Mapper.Map<DAL.Models.Core.Lesson>(Parameter);
            lesson.AuthorId = CurrentUserId;
            
            var result = UnitOfWork.GetRepository<ILessonRepository>().Update(lesson);
            OperationResult = result;
            await UnitOfWork.SaveAsync();
            
            await base.ExecuteValidated();
        }
    }
}