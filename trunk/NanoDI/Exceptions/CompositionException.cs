using System;

namespace NanoDI.Exceptions
{
    /**
     *   This exception is thrown when: 
     *   - a problem initializing the container
     *   - a cyclic dependency between components occurs.
     *   - problem adding a component
     */

    class CompositionException : Exception
    {

    }
}
