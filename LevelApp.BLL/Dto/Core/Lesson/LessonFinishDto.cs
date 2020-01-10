using System.Collections.Generic;

namespace LevelApp.BLL.Dto.Core.Lesson
{
    public class LessonFinishDto : BaseDto
    {
        public List<int> LockedLessonsIds { get; set; }

        public LessonFinishDto()
        {
            LockedLessonsIds = new List<int>();
        }
    }

}