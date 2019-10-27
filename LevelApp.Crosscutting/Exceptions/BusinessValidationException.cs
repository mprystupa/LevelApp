﻿using System;
using System.Collections.Generic;
 using System.Diagnostics.CodeAnalysis;
 using System.Net;
 using System.Runtime.Serialization;
 using System.Text;

namespace LevelApp.Crosscutting.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class BusinessValidationException : ApiException
    {
        public BusinessValidationException(string message) : base(message)
        {
        }
        
        public BusinessValidationException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        protected BusinessValidationException(SerializationInfo info, StreamingContext streamingContext)
            : base(info, streamingContext)
        {

        }
    }
}
