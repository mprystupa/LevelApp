using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.Repositories.Course;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class DeleteCourseOperation: BaseOperation<int, int>
    {
        public override async Task Validate()
        {
            if (!Repository<ICourseRepository>().CheckIfExists(x => x.Id == Parameter))
            {
                Errors.Add("Course does not exist", HttpStatusCode.NotFound);
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            OperationResult = Repository<ILessonRepository>().Delete(Parameter);
            await UnitOfWork.SaveAsync();
            await base.ExecuteValidated();
        }
    }
}