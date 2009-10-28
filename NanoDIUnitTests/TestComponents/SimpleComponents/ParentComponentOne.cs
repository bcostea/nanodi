using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoDI.Attributes;

namespace NanoDIUnitTests.TestComponents.SimpleComponents
{
    [Component("parentComponentOne")]
    class ParentComponentOne:IParentComponent
    {
        [Inject]
        IChildComponent singletonComponent;
        [Inject]
        IChildComponent prototypeComponent;


    }
}
