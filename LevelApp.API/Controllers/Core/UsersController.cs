using System.Threading.Tasks;
using LevelApp.BLL.Services.CoreUser;
using LevelApp.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevelApp.API.Controllers.Core
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        protected IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("create")]
        public ActionResult<string> CreateUser()
        {
            var newUser = new CoreUser()
            {
                Username = "Test",
                CreatedBy = 0
            };

            _userService.Create(newUser);

            return "Test";
        }
    }
}
