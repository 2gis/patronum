using System;
using System.Runtime.Serialization;

namespace Patronum.WebService.Testing.Exceptions
{
    public class LogException : Exception
    {
        public LogException()
        {
            // Add implementation.
        }

        public LogException(string message)
        {
            // Add implementation.
        }

        public LogException(string message, Exception inner)
        {
            // Add implementation.
        }

       protected LogException(SerializationInfo info, StreamingContext context)
       {
            // Add implementation.
       }
    }
}
