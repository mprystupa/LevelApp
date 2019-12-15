using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.Crosscutting.Enums.Main;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class BaseLessonOperation<TParameter, TResult> : BaseOperation<TParameter, TResult>
    {
        private Expression<Func<DAL.Models.Core.Lesson, object>> GetOrderParameter(string orderProperty)
        {
            orderProperty = orderProperty?.ToLowerInvariant();

            switch (orderProperty)
            {
                case "name":
                    return lesson => lesson.Name;
                case "author":
                    return lesson => lesson.Author.FirstName;
                default:
                    return lesson => lesson.Name;
            }
        }
            
        protected Func<IQueryable<DAL.Models.Core.Lesson>, IOrderedQueryable<DAL.Models.Core.Lesson>> LessonOrderQuery(
            LessonSearchParametersDto lessonDto)
        {
            return lessons => lessonDto.OrderDirection == "asc" ? 
                lessons.OrderBy(GetOrderParameter(lessonDto.OrderBy)) 
                : lessons.OrderByDescending(GetOrderParameter(lessonDto.OrderBy));
        }
    }
}