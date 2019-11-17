using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class DeleteLessonOperation : BaseOperation<int, int>
    {
        public override async Task Validate()
        {
            if (!Repository<ILessonRepository>().CheckIfExists(x => x.Id == Parameter))
            {
                Errors.Add("Lesson does not exist", HttpStatusCode.NotFound);
            }
            
            await base.Validate();
        }

        public override Task ExecuteValidated()
        {
            OperationResult = Repository<ILessonRepository>().Delete(Parameter);
            return base.ExecuteValidated();
        }
    }
}