using System.Threading.Tasks;
using LevelApp.API.Controllers.Base;
using LevelApp.BLL.Base;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Operations.Core.User;
using LevelApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LevelApp.API.Controllers.Core
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        public UsersController(IOperationExecutor executor) : base(executor)
        {
        }

        [HttpGet]
        [Route("create")]
        public async Task<ActionResult<int>> CreateUser(UserDto user)
        {
            return await Executor.Execute<AddUserOperation, UserDto, int>(user);
        }
    }
}
