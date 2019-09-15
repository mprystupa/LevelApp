﻿using System;

namespace LevelApp.Crosscutting.Exceptions
{
    public class GeneralServerException : Exception
    {
        public GeneralServerException(string message) : base(message)
        {
        }
    }
}