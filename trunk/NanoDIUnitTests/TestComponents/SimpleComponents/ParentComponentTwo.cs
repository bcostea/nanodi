using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NanoDI.Attributes;

namespace NanoDIUnitTests.TestComponents.SimpleComponents
{
    [Component("parentComponentTwo")]
    class ParentComponentTwo
    {
        [Inject]
        IChildComponent singletonComponent;
        [Inject]
        IChildComponent prototypeComponent;
    }
}
