using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Base;
using Microsoft.AspNetCore.Mvc;

namespace LevelApp.API.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IOperationExecutor Executor { get; }

        public BaseController(IOperationExecutor executor)
        {
            Executor = executor;
        }
    }
}
