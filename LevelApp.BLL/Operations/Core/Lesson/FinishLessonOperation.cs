using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class FinishLessonOperation : BaseOperation<LessonFinishDto, bool>
    {
        public override async Task Validate()
        {
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            var appUserLessons =
                await Repository<ILessonRepository>()
                    .GetAppUserLessonsAsync(x => x.UserId == CurrentUserId
                                            && (x.LessonId == Parameter.Id ||
                                                Parameter.LockedLessonsIds.Contains(x.LessonId)));

            var currentLesson = appUserLessons.First(x => x.LessonId == Parameter.Id);
            currentLesson.Status = LessonStatusEnum.Completed;

            var lockedLessons = appUserLessons.Where(x => x.LessonId != Parameter.Id);
            foreach (var appUserLesson in lockedLessons)
            {
                appUserLesson.Status = LessonStatusEnum.Awaiting;
            }

            Repository<ILessonRepository>().UpdateEntityBatch(appUserLessons);
            await UnitOfWork.SaveAsync();
            
            await base.ExecuteValidated();
        }
    }
}