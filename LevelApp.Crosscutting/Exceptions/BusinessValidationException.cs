﻿using System;
using System.Collections.Generic;
 using System.Runtime.Serialization;
 using System.Text;

namespace LevelApp.Crosscutting.Exceptions
{
    [Serializable]
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(string message) : base(message)
        {
        }

        protected BusinessValidationException(SerializationInfo info, StreamingContext streamingContext) 
            : base(info, streamingContext)
        {
            
        }
    }
}
