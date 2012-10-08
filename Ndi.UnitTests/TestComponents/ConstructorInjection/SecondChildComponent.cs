using System;
using Ndi.Attributes;
using Ndi.UnitTests.TestComponents.SimpleComponents;

namespace Ndi.UnitTests.TestComponents.ConstructorInjection
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
