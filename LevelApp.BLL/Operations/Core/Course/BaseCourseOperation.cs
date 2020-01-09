using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Dto.Core.Lesson;

namespace LevelApp.BLL.Operations.Core.Course
{
    public class BaseCourseOperation<TParameter, TResult> : BaseOperation<TParameter, TResult>
    {
        protected CourseSearchEntryDto MapCourseSearchEntry(DAL.Models.Core.Course course)
        {
            var courseSearchEntry = Mapper.Map<CourseSearchEntryDto>(course);
            var userCourse = course.AppUserCourses?.FirstOrDefault(x => x.UserId == CurrentUserId);

            if (userCourse == null) return courseSearchEntry;
            
            courseSearchEntry.CourseStatus = userCourse.Status;
            courseSearchEntry.IsFavourite = userCourse.IsFavourite;

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
                    return course => course.Author.FirstName;
                default:
                    return course => course.Author.FirstName;
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