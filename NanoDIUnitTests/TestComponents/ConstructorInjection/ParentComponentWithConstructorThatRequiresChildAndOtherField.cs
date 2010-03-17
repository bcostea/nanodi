using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;
using NdiUnitTests.TestComponents.SimpleComponents;

namespace NdiUnitTests.TestComponents.ConstructorInjection
{
    [Component("parentComponentWithConstructorThatRequiresChildAndOtherField")]
    class ParentComponentWithConstructorThatRequiresChildAndOtherField
    {
        IChildComponent childComponent = null;

        [Inject]
        IChildComponent secondChildComponent=null;


        public ParentComponentWithConstructorThatRequiresChildAndOtherField(IChildComponent childComponent, string bogus)
        {
            this.childComponent = childComponent;
        }

        [Inject]
        public ParentComponentWithConstructorThatRequiresChildAndOtherField(IChildComponent childComponent)
        {
            this.childComponent = childComponent;
            doSomething();
        }



        private void doSomething()
        {
            // :)
            childComponent.ToString();
        }

        public IChildComponent SecondChild { get { return secondChildComponent; } }

    }
}
