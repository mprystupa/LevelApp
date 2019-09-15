using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LevelApp.BLL.Dto
{
    [ExcludeFromCodeCoverage]
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
