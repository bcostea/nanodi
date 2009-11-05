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
    }
}
