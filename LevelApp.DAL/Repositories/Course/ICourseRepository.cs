using System.Collections.Generic;
using LevelApp.DAL.Repositories.Base;

namespace LevelApp.DAL.Repositories.Course
{
    public interface ICourseRepository: IBaseRepository<Models.Core.Course, int>
    {
        int Insert(Models.Core.Course entity, IEnumerable<Models.Core.Lesson> lessonsToJoin);
        int Update(Models.Core.Course entity, IEnumerable<Models.Core.Lesson> lessonsToJoin);
    }
}