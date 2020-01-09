using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Repositories.Course;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class UpdateCourseOperation: BaseOperation<CourseDto, int>
    {
        private DAL.Models.Core.Course _courseToUpdate; 
        public override async Task GetData()
        {
            _courseToUpdate =
                await Repository<ICourseRepository>().GetCourseWithLessonsAsync(x => x.Id == Parameter.Id);
            
            await base.GetData();
        }

        public override async Task Validate()
        {
            var existingLessonIds = Parameter.Lessons.Select(x => x.Id);

            if (!await Repository<ILessonRepository>().CheckIfAllExistByIdsAsync(existingLessonIds))
            {
                throw new BusinessValidationException("One of the lessons does not exist.", HttpStatusCode.BadRequest);
            }

            if (!Parameter.Lessons.Any())
            {
                Errors.Add("Course must contain at least one lesson.", HttpStatusCode.BadRequest); 
            }

            if (string.IsNullOrEmpty(Parameter.TreeData))
            {
                Errors.Add("Tree data cannot be empty.", HttpStatusCode.BadRequest);
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            Mapper.Map(Parameter, _courseToUpdate);
            _courseToUpdate.AuthorId = CurrentUserId;
            
            // Get lessons
            var lessons = await Repository<ILessonRepository>()
                .GetAsync(x => Parameter.Lessons.Select(y => y.Id).Contains(x.Id));
            _courseToUpdate.Lessons = lessons;
            
            var result = UnitOfWork.GetRepository<ICourseRepository>().Update(_courseToUpdate);
            OperationResult = result;
            await UnitOfWork.SaveAsync();
            
            await base.ExecuteValidated();
        }
    }
}