using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.SimpleComponents
{
    [Component("prototypeComponent", Scope.Prototype)]
    class ChildPrototypeComponent:ChildComponent
    {

    }
}
