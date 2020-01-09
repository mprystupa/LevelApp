using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Helpers.PaginatedList;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Base;

namespace LevelApp.DAL.Repositories.Course
{
    public interface ICourseRepository: IBaseRepository<Models.Core.Course, int>
    {
        Task<Models.Core.Course> GetCourseWithLessonsAsync(Expression<Func<Models.Core.Course, bool>> predicate);

        Task<Models.Core.Course> GetCourseWithRelatedDataAsync(
            Expression<Func<Models.Core.Course, bool>> predicate, int currentUserId);

        Task<Models.Core.Course>
            GetCourseWithUserCoursesDataAsync(Expression<Func<Models.Core.Course, bool>> predicate);
        Task<IPaginatedList<Models.Core.Course>> GetPaginatedCoursesAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<AppUserCourse, bool>> userCourseFilter = null,
            Expression<Func<Models.Core.Course, bool>> lessonFilter = null,
            Func<IQueryable<Models.Core.Course>, IOrderedQueryable<Models.Core.Course>> orderBy = null);
        new int Delete(int id);
    }
}