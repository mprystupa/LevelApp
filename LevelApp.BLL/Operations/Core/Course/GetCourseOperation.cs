using System.Collections.Generic;
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
            var course = await Repository<ICourseRepository>().GetCourseWithLessonsAsync(x => x.Id == Parameter);
            OperationResult = Mapper.Map<CourseDto>(course);
            OperationResult.Lessons = Mapper.Map<List<LessonCourseEntryDto>>(course.Lessons);
            
            await base.ExecuteValidated();
        }
    }
}