using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class AddFavouriteLessonOperation : BaseLessonOperation<int, bool>
    {
        private DAL.Models.Core.Lesson _lesson { get; set; }

        public override async Task Validate()
        {
            _lesson = await Repository<ILessonRepository>().GetLessonWithUserLessonsDataAsync(x => x.Id == Parameter);

            if (_lesson.AuthorId == CurrentUserId)
            {
                Errors.Add("User cannot favourite its own lesson.", System.Net.HttpStatusCode.BadRequest);
            }

            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            _lesson = SetFavourite(_lesson, true);
            await UnitOfWork.SaveAsync();
            OperationResult = true;

            await base.ExecuteValidated();
        }
    }
}
