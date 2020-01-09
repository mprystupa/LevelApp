using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.Repositories.Course;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class DeleteCourseOperation: BaseOperation<int, int>
    {
        private DAL.Models.Core.Course _course;
        public override async Task GetData()
        {
            _course = await Repository<ICourseRepository>().GetDetailAsync(x => x.Id == Parameter);
            await base.GetData();
        }

        public override async Task Validate()
        {
            if (_course.AuthorId != CurrentUserId)
            {
                Errors.Add("Course is not created by user.", HttpStatusCode.Forbidden);
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            OperationResult = Repository<ICourseRepository>().Delete(Parameter);
            await UnitOfWork.SaveAsync();
            await base.ExecuteValidated();
        }
    }
}