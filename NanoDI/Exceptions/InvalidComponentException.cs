using System;
using System.Runtime.Serialization;

namespace NanoDI.Exceptions
{
    [Serializable]
    public class InvalidComponentException : Exception
    {
        public InvalidComponentException(string componentName):
            base("No component named " + componentName + " is declared!")
        {

        }

        
        public InvalidComponentException()
        {

        }
        public InvalidComponentException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected InvalidComponentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
