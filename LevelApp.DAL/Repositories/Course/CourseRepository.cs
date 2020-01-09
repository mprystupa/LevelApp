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

namespace LevelApp.DAL.Repositories.Course
{
    public class CourseRepository: BaseRepository<Models.Core.Course, int>, ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<Models.Core.Course> GetCourseWithLessonsAsync(Expression<Func<Models.Core.Course, bool>> predicate)
        {
            var result = await Entities
                .Include(x => x.Lessons)
                .FirstOrDefaultAsync(predicate, CancellationToken.None);

            if (result == null)
            {
                throw new NotFoundException($"Entity of type {typeof(Models.Core.Course)} has not been found.", HttpStatusCode.NotFound);
            }

            return result;
        }
        
        public async Task<IPaginatedList<Models.Core.Course>> GetPaginatedCoursesAsync(
            int pageIndex, 
            int pageSize, 
            Expression<Func<AppUserCourse, bool>> userCourseFilter = null, 
            Expression<Func<Models.Core.Course, bool>> lessonFilter = null, 
            Func<IQueryable<Models.Core.Course>, IOrderedQueryable<Models.Core.Course>> orderBy = null)
        {
            IQueryable<Models.Core.Course> coursesQuery = Entities;
            IQueryable<AppUserCourse> userCoursesQuery = Context.Set<AppUserCourse>();

            if (lessonFilter != null)
            {
                coursesQuery = coursesQuery.Where(lessonFilter);
            }

            if (userCourseFilter != null)
            {
                var userLessonsQueryFilteredLessons = userCoursesQuery.Where(userCourseFilter).Select(x => x.Course);
                coursesQuery = coursesQuery.Intersect(userLessonsQueryFilteredLessons);
            }

            coursesQuery = coursesQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.AppUserCourses);
            
            var entities = await (orderBy != null ? orderBy(coursesQuery).ToListAsync() : coursesQuery.ToListAsync());
            var count = await Entities.CountAsync();
            
            return new PaginatedList<Models.Core.Course>(entities, count, pageIndex, pageSize);
        }
        
        public IList<Models.Core.Course> GetAllWithLessons()
        {
            return Entities.Include(x => x.Lessons).AsNoTracking().ToList();
        }

        public async Task<IList<Models.Core.Course>> GetAllWithLessonsAsync()
        {
            return await Entities.Include(x => x.Lessons).AsNoTracking().ToListAsync();
        }

        public int Insert(Models.Core.Course entity, IEnumerable<Models.Core.Lesson> lessonsToJoin)
        {
            entity.Lessons = new List<Models.Core.Lesson>();
            var lessonIds = lessonsToJoin.Select(x => x.Id);
            var lessonsToUpdate = Context.Set<Models.Core.Lesson>().Where(x => lessonIds.Contains(x.Id)).ToList();

            foreach (var lesson in lessonsToUpdate)
            {
                lesson.Course = entity;
            }

            Entities.Add(entity);
            return entity.Id;
        }

        public new int Delete(int id)
        {
            var entity = Entities.Include(x => x.Lessons).First(x => x.Id == id);
            
            foreach (var entityLesson in entity.Lessons)
            {
                entityLesson.CourseId = null;
            }
            
            return Delete(entity);
        }
    }
}