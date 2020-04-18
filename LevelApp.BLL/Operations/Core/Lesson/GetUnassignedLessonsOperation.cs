using System.Collections.Generic;
using System.Linq;
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

            var resultsMapped = Mapper.Map<List<LessonCourseEntryDto>>(results);
            
            foreach (var result in resultsMapped.Where(result => !string.IsNullOrEmpty(result.IconUrl)))
            {
                result.IconUrl = FileService.GetImageAsDataUri(result.IconUrl);
            }

            OperationResult = resultsMapped;
            
            await base.ExecuteValidated();
        }
    }
}