using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoDI.Attributes;

namespace NanoDIUnitTests.TestComponents.SimpleComponents
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
