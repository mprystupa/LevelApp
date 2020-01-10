using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.Crosscutting.Helpers.PaginatedList;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LevelApp.DAL.Repositories.Lesson
{
    public class LessonRepository : BaseRepository<Models.Core.Lesson, int>, ILessonRepository
    {
        public LessonRepository(DbContext context) : base(context)
        {
        }

        public async Task<IPaginatedList<Models.Core.Lesson>> GetPaginatedLessonsAsync(
            int pageIndex, 
            int pageSize, 
            Expression<Func<AppUserLesson, bool>> userLessonFilter = null, 
            Expression<Func<Models.Core.Lesson, bool>> lessonFilter = null, 
            Func<IQueryable<Models.Core.Lesson>, IOrderedQueryable<Models.Core.Lesson>> orderBy = null)
        {
            IQueryable<Models.Core.Lesson> lessonsQuery = Entities;
            IQueryable<AppUserLesson> userLessonsQuery = Context.Set<AppUserLesson>();

            if (lessonFilter != null)
            {
                lessonsQuery = lessonsQuery.Where(lessonFilter);
            }

            if (userLessonFilter != null)
            {
                var userLessonsQueryFilteredLessons = userLessonsQuery.Where(userLessonFilter).Select(x => x.Lesson);
                lessonsQuery = lessonsQuery.Intersect(userLessonsQueryFilteredLessons);
            }

            lessonsQuery = lessonsQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.AppUserLessons);
            
            var entities = await (orderBy != null ? orderBy(lessonsQuery).ToListAsync() : lessonsQuery.ToListAsync());
            var count = await Entities.CountAsync();
            
            return new PaginatedList<Models.Core.Lesson>(entities, count, pageIndex, pageSize);
        }

        public async Task<Models.Core.Lesson> GetLessonWithUserLessonsDataAsync(Expression<Func<Models.Core.Lesson, bool>> predicate)
        {
            var result = await Entities
                .Include(x => x.AppUserLessons)
                .FirstOrDefaultAsync(predicate, CancellationToken.None);

            if (result == null)
            {
                throw new NotFoundException($"Entity of type {typeof(Models.Core.Lesson)} has not been found.", HttpStatusCode.NotFound);
            }

            return result;
        }

        public async Task<List<AppUserLesson>> GetAppUserLessonsAsync(
            Expression<Func<Models.Core.AppUserLesson, bool>> predicate)
        {
            IQueryable<AppUserLesson> query = Context.Set<AppUserLesson>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
    }
}