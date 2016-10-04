using Xunit;
using Ndi;
using Ndi.UnitTests.TestComponents.ConstructorInjection;


namespace Ndi.UnitTests
{
    
    public class XmlApplicationContextConstructorInjectionTests
    {
        protected IApplicationContext applicationContext;

        [Fact]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext = new XmlApplicationContext("constructorinjection.xml");

            Assert.NotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [Fact]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext = new XmlApplicationContext("constructorinjection.xml");

            Assert.NotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField"));
            Assert.NotNull(((ParentComponentWithConstructorThatRequiresChildAndOtherField)applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField")).SecondChild);
        }
    }
}
