using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.FieldRequiredInConstructor
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
