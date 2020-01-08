using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Repositories.Course
{
    public class CourseRepository: BaseRepository<Models.Core.Course, int>, ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {
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
        
        public int Update(Models.Core.Course entity, IEnumerable<Models.Core.Lesson> lessonsToJoin)
        {
            var toJoin = lessonsToJoin.ToList();
            var existingEntityLessons = Entities.Include(x => x.Lessons).First(x => x.Id == entity.Id).Lessons;
            
            foreach (var existingEntityLesson in existingEntityLessons)
            {
                existingEntityLesson.CourseId = null;
            }
            
            Context.AttachRange(existingEntityLessons);
            
            foreach (var lesson in toJoin)
            {
                entity.Lessons.Add(lesson);
            }

            Entities.Attach(entity);
            return Entities.Update(entity).Entity.Id;
        }
    }
}