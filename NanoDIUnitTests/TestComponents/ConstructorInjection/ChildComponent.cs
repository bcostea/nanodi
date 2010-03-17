using System;
using Ndi.Attributes;
using NdiUnitTests.TestComponents.SimpleComponents;

namespace NdiUnitTests.TestComponents.ConstructorInjection
{
    [Component("childComponent")]
    class ChildComponent :IChildComponent
    {
        string content = "I am not null";

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
