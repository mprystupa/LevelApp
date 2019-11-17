using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Repositories.Lesson
{
    public class LessonRepository : BaseRepository<Models.Core.Lesson, int>, ILessonRepository
    {
        public LessonRepository(DbContext context) : base(context)
        {
        }
    }
}