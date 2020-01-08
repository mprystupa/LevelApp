using System.Collections.Generic;
using System.Threading.Tasks;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class GetUnassignedLessonsOperation : BaseLessonOperation<int, List<LessonCourseEntryDto>>
    {
        public override async Task ExecuteValidated()
        {
            var results = await Repository<ILessonRepository>()
                .GetAsync(
                    lesson => lesson.CreatedBy == CurrentUserId 
                              && lesson.CourseId == null
                );

            OperationResult = Mapper.Map<List<LessonCourseEntryDto>>(results);
            await base.ExecuteValidated();
        }
    }
}