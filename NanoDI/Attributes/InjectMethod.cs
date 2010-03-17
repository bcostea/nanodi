using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi.Attributes
{
    public enum InjectMethod
    {
        None=0,
        Field=1,
        Constructor=2,
        Setter=3
    }
}
