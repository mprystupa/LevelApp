using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class BaseCourseOperation<TParameter, TResult> : BaseOperation<TParameter, TResult>
    {
        protected CourseSearchEntryDto MapCourseSearchEntry(DAL.Models.Core.Course course)
        {
            var courseSearchEntry = Mapper.Map<CourseSearchEntryDto>(course);
            var userCourse = course.AppUserCourses?.FirstOrDefault(x => x.UserId == CurrentUserId);

            courseSearchEntry.CourseStatus = userCourse?.Status ?? CourseStatusEnum.NotStarted;
            courseSearchEntry.IsFavourite = userCourse?.IsFavourite ?? false;

            return courseSearchEntry;
        }

        protected List<CourseSearchEntryDto> MapCourseSearchEntry(IEnumerable<DAL.Models.Core.Course> courses)
        {
            return courses.Select(MapCourseSearchEntry).ToList();
        }
        
        private Expression<Func<DAL.Models.Core.Course, object>> GetOrderParameter(string orderProperty)
        {
            orderProperty = orderProperty?.ToLowerInvariant();

            switch (orderProperty)
            {
                case "name":
                    return course => course.Name;
                case "author":
                    return course => course.Author.DisplayName;
                default:
                    return course => course.Author.DisplayName;
            }
        }
            
        protected Func<IQueryable<DAL.Models.Core.Course>, IOrderedQueryable<DAL.Models.Core.Course>> CourseOrderQuery(
            CourseSearchParametersDto courseDto)
        {
            return lessons => courseDto.OrderDirection == "asc" ? 
                lessons.OrderBy(GetOrderParameter(courseDto.OrderBy)) 
                : lessons.OrderByDescending(GetOrderParameter(courseDto.OrderBy));
        }
    }
}