using System;

namespace NanoDI.Exceptions
{
    class InvalidComponentException:Exception
    {
        public InvalidComponentException(string componentName):
            base("No component named " + componentName + " is declared!")
        {

        }
    }
}
