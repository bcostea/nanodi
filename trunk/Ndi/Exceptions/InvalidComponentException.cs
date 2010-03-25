using System;
using System.Runtime.Serialization;

namespace Ndi.Exceptions
{
    [Serializable]
    public class InvalidComponentException : Exception
    {
        public InvalidComponentException(string componentName) :
            base("No component named " + componentName + " is declared!")
        {
        }
    }
}
