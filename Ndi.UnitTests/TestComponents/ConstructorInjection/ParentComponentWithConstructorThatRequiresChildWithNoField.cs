using System;
using Ndi.Attributes;
using Ndi.UnitTests.TestComponents.SimpleComponents;

namespace Ndi.UnitTests.TestComponents.ConstructorInjection
{

    [Component("parentComponentWithConstructorThatRequiresChildWithNoField")]
    class ParentComponentWithConstructorThatRequiresChildWithNoField
    {
        private String componentText;

        [Inject]
        public ParentComponentWithConstructorThatRequiresChildWithNoField(IChildComponent childComponent)
        {
            this.componentText = childComponent.ToString();
            doSomething();
        }



        private void doSomething()
        {
            componentText += componentText.Length; // :)
        }
    }
}
