using System;
using System.Runtime.Serialization;

namespace Ndi.Exceptions
{
    [Serializable]
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException(string components) :
            base("Circular dependency between components: " + components + " !")
        {

        }

        public CircularDependencyException()
        {

        }
        public CircularDependencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected CircularDependencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
