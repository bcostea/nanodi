using System;
using System.Runtime.Serialization;

namespace NanoDI.Exceptions
{
    /**
     *   This exception is thrown when: 
     *   - a problem initializing the container
     *   - a cyclic dependency between components occurs.
     *   - problem adding a component
     */

    [Serializable]
    public class CompositionException : Exception
    {
        public CompositionException()
        {

        }
        public CompositionException(string message)
            : base(message)
        {
        }
        public CompositionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected CompositionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

    }
}
