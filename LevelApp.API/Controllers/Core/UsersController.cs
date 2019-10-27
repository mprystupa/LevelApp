using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using LevelApp.API.Controllers.Base;
using LevelApp.API.Routes;
using LevelApp.BLL.Base;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.BLL.Operations.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevelApp.API.Controllers.Core
{
    [Authorize]
    [Route(BaseRoutes.Controller)]
    public class UsersController : BaseController
    {
        public UsersController(IOperationExecutor executor) : base(executor)
        {
        }
        
        [HttpGet]
        [Route(BaseRoutes.Root)]
        public async Task<ActionResult<List<UserSearchDto>>> GetAllUsers()
        {
            return await Executor.Execute<GetAllUsersOperation, int, List<UserSearchDto>> (0);
        }
        
        [HttpGet]
        [Route(BaseRoutes.Id)]
        public async Task<ActionResult<UserSearchDto>> GetUser(int id)
        {
            return await Executor.Execute<GetUserOperation, int, UserSearchDto>(id);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route(UserRoutes.Login)]
        public async Task<ActionResult<string>> Login([FromBody]UserLoginDto login)
        {
            return await Executor.Execute<AuthenticateUserOperation, UserLoginDto, string>(login);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route(UserRoutes.Register)]
        public async Task<ActionResult<int>> RegisterNewUser([FromBody]UserRegisterDto user)
        {
            return await Executor.Execute<RegisterUserOperation, UserRegisterDto, int>(user);
        }
    }
}
