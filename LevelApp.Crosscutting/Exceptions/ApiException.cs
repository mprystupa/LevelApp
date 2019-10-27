using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace LevelApp.Crosscutting.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode;
        
        public ApiException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        
        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}