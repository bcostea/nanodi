using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;
using Ndi.UnitTests.TestComponents.SimpleComponents;

namespace Ndi.UnitTests.TestComponents.ConstructorInjection
{
    [Component("parentComponentWithConstructorThatRequiresChild")]
    class ParentComponentWithConstructorThatRequiresChild
    {
        
        IChildComponent childComponent = null;


        public ParentComponentWithConstructorThatRequiresChild(IChildComponent childComponent, string bogus)
        {
            this.childComponent = childComponent;
        }

        [Inject]
        public ParentComponentWithConstructorThatRequiresChild(IChildComponent childComponent)
        {
            this.childComponent = childComponent;
            doSomething();
        }



        private void doSomething()
        {
            // :)
            childComponent.ToString();
        }

    }
}
