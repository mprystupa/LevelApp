﻿using System;
 using System.Runtime.Serialization;

 namespace LevelApp.Crosscutting.Exceptions
{
    [Serializable]
    public class GeneralServerException : Exception
    {
        public GeneralServerException(string message) : base(message)
        {
        }

        protected GeneralServerException(SerializationInfo info, StreamingContext streamingContext)
            : base(info, streamingContext)
        {
        }
    }
}