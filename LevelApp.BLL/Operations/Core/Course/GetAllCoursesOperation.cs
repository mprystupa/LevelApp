using System.Collections.Generic;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.DAL.Repositories.Course;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class GetAllCoursesOperation : BaseOperation<int, List<CourseDto>>
    {
        public override async Task ExecuteValidated()
        {
            var allCourses = await UnitOfWork.GetRepository<ICourseRepository>().GetAllAsync();
            OperationResult = Mapper.Map<List<CourseDto>>(allCourses);
            await base.ExecuteValidated();
        }
    }
}