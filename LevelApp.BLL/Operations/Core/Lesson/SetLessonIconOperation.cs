using System.Threading.Tasks;
using LevelApp.BLL.Dto;
using LevelApp.DAL.Repositories.Lesson;
using Microsoft.AspNetCore.Http;

namespace LevelApp.BLL.Operations.Core.Lesson
{
    public class SetLessonIconOperation : BaseLessonOperation<FileDto, int>
    {
        private DAL.Models.Core.Lesson Lesson { get; set; }
        public override async Task GetData()
        {
            Lesson = await Repository<ILessonRepository>().GetDetailAsync(x => x.Id == Parameter.Id);
            await base.GetData();
        }

        public override async Task ExecuteValidated()
        { 
            var iconDirectory = $"lessons\\{Parameter.Id}";
            Lesson.IconUrl = await FileService.SaveFile(Parameter.File, iconDirectory, "icon");
            Repository<ILessonRepository>().Update(Lesson);
            await UnitOfWork.SaveAsync();

            OperationResult = Lesson.Id;
            await base.ExecuteValidated();
        }
    }
}