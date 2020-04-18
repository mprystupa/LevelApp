using System.Collections.Generic;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class AddLessonOperation : BaseOperation<LessonDto, int>
    {
        public override async Task ExecuteValidated()
        {
            var lesson = Mapper.Map<DAL.Models.Core.Lesson>(Parameter);
            lesson.AuthorId = CurrentUserId;
            
            lesson.AppUserLessons = new List<AppUserLesson>()
            {
                new AppUserLesson()
                {
                    UserId = CurrentUserId,
                    IsFavourite = false,
                    Status = LessonStatusEnum.Created
                }
            };
            
            Repository<ILessonRepository>().Insert(lesson);
            await UnitOfWork.SaveAsync();

            OperationResult = lesson.Id;

            await base.ExecuteValidated();
        }
    }
}