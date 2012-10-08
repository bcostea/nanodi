using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi.UnitTests.TestComponents.CircularDependencies
{
    interface ICircularDependency
    {
        ICircularDependency OtherDependency{get;}
    }
}
