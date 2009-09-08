using System;

namespace NanoDI.Exceptions
{
    class CircularDependencyException : Exception
    {
        public CircularDependencyException(string components) :
            base("Circular dependency between components: " + components + " !")
        {

        }
    }
}
