using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LevelApp.BLL.Dto
{
    [ExcludeFromCodeCoverage]
    public class BaseDto
    {
        public BaseDto()
        {
            Permissions = new Dictionary<string, bool>();
        }

        public int Id { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
    }
}