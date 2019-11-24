using System.Collections.Generic;
using System.Threading.Tasks;
using LevelApp.API.Controllers.Base;
using LevelApp.API.Routes;
using LevelApp.BLL.Base;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.BLL.Operations.Core.Lesson;
using LevelApp.BLL.Operations.Core.User;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.DAL.Models.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevelApp.API.Controllers.Core
{
    [Authorize]
    [Route(BaseRoutes.Controller)]
    public class LessonsController : BaseController
    {
        public LessonsController(IOperationExecutor executor) : base(executor)
        {
        }
        
        [HttpGet(BaseRoutes.Root)]
        public async Task<ActionResult<List<LessonDto>>> GetAllLessons()
        {
            return await Executor.Execute<GetAllLessonsOperation, int, List<LessonDto>>(0);
        }

        [HttpGet(LessonRoutes.SearchCreated)]
        public async Task<ActionResult<LessonSearchResultsDto>> SearchCreatedLessons([FromQuery] int? pageIndex)
        {
            var parameter = new LessonSearchCreatedParametersDto()
            {
                PageSize = 4,
                PageIndex = pageIndex ?? 1
            };

            return await Executor.Execute<SearchCreatedLessonsOperation, LessonSearchCreatedParametersDto, LessonSearchResultsDto>(
                parameter);
        }
        
        [HttpGet(LessonRoutes.SearchFavourite)]
        public async Task<ActionResult<LessonSearchResultsDto>> SearchFavouriteLessons([FromQuery] int? pageIndex)
        {
            var parameter = new LessonSearchAttendingParametersDto()
            {
                PageSize = 4,
                PageIndex = pageIndex ?? 1,
                IsFavourite = true
            };

            return await Executor.Execute<SearchAttendingLessonsOperation, LessonSearchAttendingParametersDto, LessonSearchResultsDto>(
                parameter);
        }
        
        [HttpGet(LessonRoutes.SearchCompleted)]
        public async Task<ActionResult<LessonSearchResultsDto>> SearchCompletedLessons([FromQuery] int? pageIndex)
        {
            var parameter = new LessonSearchAttendingParametersDto()
            {
                PageSize = 4,
                PageIndex = pageIndex ?? 1,
                LessonStatus = LessonStatusEnum.Completed
            };

            return await Executor.Execute<SearchAttendingLessonsOperation, LessonSearchAttendingParametersDto, LessonSearchResultsDto>(
                parameter);
        }
        
        [HttpGet(LessonRoutes.SearchAwaiting)]
        public async Task<ActionResult<LessonSearchResultsDto>> SearchAwaitingLessons([FromQuery] int? pageIndex)
        {
            var parameter = new LessonSearchAttendingParametersDto()
            {
                PageSize = 4,
                PageIndex = pageIndex ?? 1,
                LessonStatus = LessonStatusEnum.Awaiting
            };

            return await Executor.Execute<SearchAttendingLessonsOperation, LessonSearchAttendingParametersDto, LessonSearchResultsDto>(
                parameter);
        }

        [HttpGet(BaseRoutes.Id)]
        public async Task<ActionResult<LessonDto>> GetLesson(int id)
        {
            return await Executor.Execute<GetLessonOperation, int, LessonDto>(id);
        }

        [HttpPost(BaseRoutes.Root)]
        public async Task<ActionResult<int>> AddLesson([FromBody] LessonDto lesson)
        {
            return await Executor.Execute<AddLessonOperation, LessonDto, int>(lesson);
        }

        [HttpPut(BaseRoutes.Id)]
        public async Task<ActionResult<int>> UpdateLesson(int id, [FromBody] LessonDto lesson)
        {
            return await Executor.Execute<UpdateLessonOperation, LessonDto, int>(lesson);
        }

        [HttpDelete(BaseRoutes.Id)]
        public async Task<ActionResult<int>> DeleteLesson(int id)
        {
            return await Executor.Execute<DeleteLessonOperation, int, int>(id);
        }
    }
}