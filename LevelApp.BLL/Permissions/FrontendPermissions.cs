using System;
using System.Collections.Generic;
using System.Text;

namespace LevelApp.BLL.Permissions
{
    public static class FrontendPermissions
    {
        public static string CanEdit => nameof(CanEdit);
        public static string CanContinue => nameof(CanContinue);
        public static string CanAttend => nameof(CanAttend);
    }
}
