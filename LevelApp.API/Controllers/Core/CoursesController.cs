using System.Collections.Generic;
using System.Threading.Tasks;
using LevelApp.API.Controllers.Base;
using LevelApp.API.Routes;
using LevelApp.BLL.Base;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Operations.Core.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevelApp.API.Controllers.Core
{
    [Authorize]
    [Route(BaseRoutes.Controller)]
    public class CoursesController : BaseController
    {
        public CoursesController(IOperationExecutor executor) : base(executor)
        {
        }
        
        [HttpGet(BaseRoutes.Root)]
        public async Task<ActionResult<List<CourseDto>>> GetAllLessons()
        {
            return await Executor.Execute<GetAllCoursesOperation, int, List<CourseDto>>(0);
        }
        
        [HttpGet(BaseRoutes.Id)]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            return await Executor.Execute<GetCourseOperation, int, CourseDto>(id);
        }
        
        [HttpGet(CourseRoutes.Search)]
        public async Task<ActionResult<CourseSearchResultsDto>> SearchAllLessons([FromQuery] CourseSearchParametersDto parameter)
        {
            return await Executor.Execute<SearchAllCoursesOperation, CourseSearchParametersDto, CourseSearchResultsDto>(
                parameter);
        }
        
        [HttpGet(CourseRoutes.SearchCreated)]
        public async Task<ActionResult<CourseSearchResultsDto>> SearchCreatedLessons([FromQuery] CourseSearchParametersDto parameter)
        {
            parameter.IsCreatedByUserOnly = true;

            return await Executor.Execute<SearchCreatedCoursesOperation, CourseSearchParametersDto, CourseSearchResultsDto>(
                parameter);
        }

        [HttpPost(BaseRoutes.Root)]
        public async Task<ActionResult<int>> AddCourse([FromBody] CourseDto course)
        {
            return await Executor.Execute<AddCourseOperation, CourseDto, int>(course);
        }

        [HttpPut(BaseRoutes.Id)]
        public async Task<ActionResult<int>> UpdateCourse(int id, [FromBody] CourseDto course)
        {
            return await Executor.Execute<UpdateCourseOperation, CourseDto, int>(course);
        }

        [HttpDelete(BaseRoutes.Id)]
        public async Task<ActionResult<int>> DeleteCourse(int id)
        {
            return await Executor.Execute<DeleteCourseOperation, int, int>(id);
        }
    }
}