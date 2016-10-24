using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.InjectByType
{
    [Component("injectChildComponent")]
    public class Child
    {
        public string GetName()
        {
            return "injectChildComponent";
        }
    }
}
