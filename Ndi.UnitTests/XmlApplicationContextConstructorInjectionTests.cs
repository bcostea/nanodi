using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi;
using Ndi.UnitTests.TestComponents.ConstructorInjection;


namespace Ndi.UnitTests
{
    [TestClass]
    public class XmlApplicationContextConstructorInjectionTests
    {
        protected IApplicationContext applicationContext;

        [TestMethod]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext = new XmlApplicationContext("constructorinjection.xml");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [TestMethod]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext = new XmlApplicationContext("constructorinjection.xml");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField"));
            Assert.IsNotNull(((ParentComponentWithConstructorThatRequiresChildAndOtherField)applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField")).SecondChild);
        }
    }
}
