using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LevelApp.BLL.Dto
{
    [ExcludeFromCodeCoverage]
    public class BaseDto
    {
        public int Id { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
    }
}