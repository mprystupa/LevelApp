using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.DAL.Repositories.Course;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class UpdateCourseOperation: BaseOperation<CourseDto, int>
    {
        public override async Task Validate()
        {
            if (!Repository<ICourseRepository>().CheckIfExists(x => x.Id == Parameter.Id))
            {
                Errors.Add("Course does not exist.", HttpStatusCode.NotFound);
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            var course = Mapper.Map<DAL.Models.Core.Course>(Parameter);
            course.AuthorId = CurrentUserId;
            
            var lessons = Mapper.Map<List<DAL.Models.Core.Lesson>>(Parameter.Lessons);
            
            var result = UnitOfWork.GetRepository<ICourseRepository>().Update(course, lessons);
            OperationResult = result;
            await UnitOfWork.SaveAsync();
            
            await base.ExecuteValidated();
        }
    }
}