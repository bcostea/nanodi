using Ndi;
using NdiUnitTests.TestComponents.ConstructorInjection;
using NUnit.Framework;

namespace NdiUnitTests
{
    [TestFixture()]
    public class XmlApplicationContextConstructorInjectionTests
    {
        protected IApplicationContext applicationContext;

        [Test]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext = new XmlApplicationContext("constructorinjection.xml");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [Test]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext = new XmlApplicationContext("constructorinjection.xml");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField"));
            Assert.IsNotNull(((ParentComponentWithConstructorThatRequiresChildAndOtherField)applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField")).SecondChild);
        }
    }
}
