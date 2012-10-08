using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.SimpleComponents
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
