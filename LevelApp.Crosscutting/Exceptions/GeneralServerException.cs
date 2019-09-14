using System;

namespace LevelApp.BLL.Exceptions
{
    public class GeneralServerException : Exception
    {
        public GeneralServerException(string message) : base(message)
        {
        }
    }
}