﻿using System;
 using System.Net;
 using System.Runtime.Serialization;

 namespace LevelApp.Crosscutting.Exceptions
{
    [Serializable]
    public class GeneralServerException : ApiException
    {
        public GeneralServerException(string message) : base(message)
        {
        }
        
        public GeneralServerException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        protected GeneralServerException(SerializationInfo info, StreamingContext streamingContext)
            : base(info, streamingContext)
        {
        }
    }
}