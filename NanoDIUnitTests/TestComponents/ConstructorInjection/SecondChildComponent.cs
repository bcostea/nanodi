using System;
using Ndi.Attributes;
using NdiUnitTests.TestComponents.SimpleComponents;

namespace NdiUnitTests.TestComponents.ConstructorInjection
{
    [Component("secondChildComponent")]
    class SecondChildComponent :IChildComponent
    {
        string content = "I am the second child";

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                this.content = value;
            }
        }

    }
}
