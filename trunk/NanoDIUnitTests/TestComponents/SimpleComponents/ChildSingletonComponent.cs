using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;

namespace NdiUnitTests.TestComponents.SimpleComponents
{
    [Component("singletonComponent")]
    class ChildSingletonComponent:ChildComponent
    {
        public ChildSingletonComponent()
        {
            Content = "Singleton";
        }
    }
}
