using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Course;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class GetCourseOperation: BaseOperation<int, CourseDto>
    {
        public override async Task ExecuteValidated()
        {
            var course = await Repository<ICourseRepository>().GetCourseWithRelatedDataAsync(x => x.Id == Parameter, CurrentUserId);
            var lessons = course.Lessons;
            
            OperationResult = Mapper.Map<CourseDto>(course);

            if (lessons != null)
            {
                foreach (var lesson in lessons)
                {
                    OperationResult.Lessons.Add(new LessonCourseEntryDto()
                    {
                        Id = lesson.Id,
                        Name = lesson.Name,
                        Description = lesson.Description,
                        IsFirst = lesson.IsFirst ?? false,
                        Status = lesson.AppUserLessons.First(x => x.UserId == CurrentUserId).Status
                    });
                }
            }

            await base.ExecuteValidated();
        }
    }
}