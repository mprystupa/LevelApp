using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Course;
using LevelApp.DAL.Repositories.Lesson;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class AddCourseOperation: BaseOperation<CourseDto, int>
    {
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
            var course = Mapper.Map<DAL.Models.Core.Course>(Parameter);
            course.AuthorId = CurrentUserId;
            
            course.AppUserCourses = new List<AppUserCourse>()
            {
                new AppUserCourse()
                {
                    UserId = CurrentUserId,
                    IsFavourite = false,
                    Status = CourseStatusEnum.Created
                }
            };

            var lessons = Mapper.Map<List<DAL.Models.Core.Lesson>>(Parameter.Lessons);

            OperationResult = Repository<ICourseRepository>().Insert(course, lessons);
            await UnitOfWork.SaveAsync();
            
            await base.ExecuteValidated();
        }
    }
}