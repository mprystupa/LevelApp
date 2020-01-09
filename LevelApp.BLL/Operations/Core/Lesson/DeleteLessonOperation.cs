using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class DeleteLessonOperation : BaseOperation<int, int>
    {
        private DAL.Models.Core.Lesson _lesson;
        public override async Task GetData()
        {
            _lesson = await Repository<ILessonRepository>().GetDetailAsync(x => x.Id == Parameter);
            await base.GetData();
        }

        public override async Task Validate()
        {
            if (_lesson.CourseId != null)
            {
                Errors.Add("Lesson is assigned to course.", HttpStatusCode.Conflict);
            }

            if (_lesson.AuthorId != CurrentUserId)
            {
                Errors.Add("Lesson is not created by user.", HttpStatusCode.Forbidden);
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