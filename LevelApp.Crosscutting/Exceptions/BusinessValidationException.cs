﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LevelApp.Crosscutting.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(string message) : base(message)
        {
        }
    }
}
