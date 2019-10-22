﻿using System;
using System.Net;
using System.Runtime.Serialization;

namespace LevelApp.Crosscutting.Exceptions
{
    [Serializable]
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
        }
        
        public NotFoundException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }
        
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}