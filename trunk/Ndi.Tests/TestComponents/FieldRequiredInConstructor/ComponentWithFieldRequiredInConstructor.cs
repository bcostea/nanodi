using Ndi.Attributes;

namespace NdiUnitTests.TestComponents.FieldRequiredInConstructor
{
    [Component("componentWithFieldRequiredInConstructor")]
    class ComponentWithFieldRequiredInConstructor
    {
        [Inject]
        ComponentRequiredByConstructor requiredComponent=null;

        public ComponentWithFieldRequiredInConstructor()
        {
            requiredComponent.ToString();
        }
    }
}
