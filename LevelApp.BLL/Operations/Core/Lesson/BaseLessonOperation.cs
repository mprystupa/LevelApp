using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Permissions;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.DAL.Models.Core;
using Microsoft.AspNetCore.Hosting.Internal;

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
                    return lesson => lesson.Author.DisplayName;
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

        protected List<LessonSearchEntryDto> AddLessonsFrontendPermissions(List<LessonSearchEntryDto> lessons)
        {
            foreach (var lesson in lessons.Where(lesson => lesson.Author != null && lesson.Author.Id == CurrentUserId))
            {
                lesson.Permissions.Add(FrontendPermissions.CanEdit, true);
            }

            return lessons;
        }

        protected LessonSearchEntryDto MapLessonSearchEntry(DAL.Models.Core.Lesson lesson)
        {
            var lessonSearchEntry = Mapper.Map<LessonSearchEntryDto>(lesson);
            var userLesson = lesson.AppUserLessons?.FirstOrDefault(x => x.UserId == CurrentUserId);

            if (lesson.Author != null)
            {
                lessonSearchEntry.Author = Mapper.Map<LessonSearchEntryAuthorDto>(lesson.Author);
            }

            if (lesson.Course != null)
            {
                lessonSearchEntry.Course = Mapper.Map<LessonSearchEntryCourseDto>(lesson.Course);
            }
            
            if (userLesson == null) return lessonSearchEntry;
            
            lessonSearchEntry.LessonStatus = userLesson.Status;
            lessonSearchEntry.IsFavourite = userLesson.IsFavourite;

            if (!string.IsNullOrEmpty(lesson.IconUrl))
            {
                lessonSearchEntry.IconUrl = FileService.GetImageAsDataUri(lesson.IconUrl);
            }
            return lessonSearchEntry;
        }

        protected List<LessonSearchEntryDto> MapLessonSearchEntry(IEnumerable<DAL.Models.Core.Lesson> lessons)
        {
            return lessons.Select(MapLessonSearchEntry).ToList();
        }

        protected DAL.Models.Core.Lesson SetFavourite(DAL.Models.Core.Lesson lesson, bool isFavourite)
        {
            AppUserLesson userLesson;

            if (lesson.AppUserLessons != null && lesson.AppUserLessons.Any(x => x.UserId == CurrentUserId))
            {
               userLesson = lesson.AppUserLessons.First(x => x.UserId == CurrentUserId);
            }
            else
            {
                userLesson = new AppUserLesson()
                {
                    UserId = CurrentUserId,
                    LessonId = lesson.Id,
                    Status = LessonStatusEnum.NotStarted
                };

                lesson.AppUserLessons.Add(userLesson);
            }

            userLesson.IsFavourite = isFavourite;

            return lesson;
        }
    }
}