using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;

namespace NdiUnitTests.TestComponents.SimpleComponents
{
    [Component("prototypeComponent", Scope.Prototype)]
    class ChildPrototypeComponent:ChildComponent
    {

    }
}
