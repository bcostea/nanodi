using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.InjectByType
{
    [Component("injectParentComponent")]
    public class Parent
    {
        public string ChildName {get; set;}

        [Inject]
        public Parent(Child child)
        {
            ChildName = child.GetName();
        }
    }
}
