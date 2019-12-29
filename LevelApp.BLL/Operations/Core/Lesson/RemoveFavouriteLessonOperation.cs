using LevelApp.DAL.Repositories.Lesson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class RemoveFavouriteLessonOperation : BaseLessonOperation<int, bool>
    {
        private DAL.Models.Core.Lesson _lesson { get; set; }

        public override async Task Validate()
        {
            _lesson = await Repository<ILessonRepository>().GetLessonWithUserLessonsDataAsync(x => x.Id == Parameter);

            if (_lesson.AuthorId == CurrentUserId)
            {
                Errors.Add("User cannot unfavourite its own lesson.", System.Net.HttpStatusCode.BadRequest);
            }

            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            _lesson = SetFavourite(_lesson, false);
            await UnitOfWork.SaveAsync();
            OperationResult = false;

            await base.ExecuteValidated();
        }
    }
}
