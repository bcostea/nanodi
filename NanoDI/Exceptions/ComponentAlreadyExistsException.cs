using System;
using System.Runtime.Serialization;

namespace NanoDI.Exceptions
{
    [Serializable]
    public class ComponentAlreadyExistsException : Exception
    {
        public ComponentAlreadyExistsException(string componentName) :
            base("A component named [" + componentName + "] is already declared!")
        {

        }


        public ComponentAlreadyExistsException()
        {

        }
        public ComponentAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected ComponentAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
