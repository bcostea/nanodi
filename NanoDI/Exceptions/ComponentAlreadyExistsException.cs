using System;

namespace NanoDI.Exceptions
{
    class ComponentAlreadyExistsException:Exception
    {
        public ComponentAlreadyExistsException(string componentName) :
            base("A component named [" + componentName + "] is already declared!")
        {

        }
    }
}
