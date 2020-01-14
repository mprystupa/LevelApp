using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Course;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class AddAttendingCourseOperation : BaseOperation<int, bool>
    {
        public override async Task Validate()
        {
            // TODO: Poprawić pobieranie z bazy jednym zapytaniem!
            var course = await Repository<ICourseRepository>().GetCourseWithUserCoursesDataAsync(x => x.Id == Parameter);

            if (course.AppUserCourses.Select(y => y.UserId).Contains(CurrentUserId))
            {
                Errors.Add("User already attending this course.", HttpStatusCode.Conflict);
            }
        }

        public override async Task ExecuteValidated()
        {
            var course = await Repository<ICourseRepository>().GetCourseWithLessonsAsync(x => x.Id == Parameter);
            var firstLesson = course.Lessons.First(x => x.IsFirst == true);
            var otherLessons = course.Lessons.Where(x => x.Id != firstLesson.Id);

            var appUserCourse = new AppUserCourse()
            {
                UserId = CurrentUserId,
                CourseId = course.Id,
                Status = CourseStatusEnum.InProgress
            };

            // Add first lesson data
            var appUserLessons = new List<AppUserLesson>
            {
                new AppUserLesson()
                {
                    UserId = CurrentUserId, 
                    LessonId = firstLesson.Id,
                    Status = LessonStatusEnum.Awaiting
                }
            };

            // Other lessons data
            appUserLessons.AddRange(
                otherLessons.Select(
                    otherLesson => new AppUserLesson()
                    {
                        UserId = CurrentUserId, 
                        LessonId = otherLesson.Id, 
                        Status = LessonStatusEnum.Locked
                    }));

            Repository<ILessonRepository>().InsertEntityBatch(appUserLessons);
            Repository<ICourseRepository>().InsertEntity(appUserCourse);
            await UnitOfWork.SaveAsync();

            OperationResult = true;
        }
    }
}