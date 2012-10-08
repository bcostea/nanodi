using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi;

namespace Ndi.UnitTests
{
    [TestClass]
    public class AppTest
    {
        private IApplicationContext applicationContext;

        [TestInitialize]
        public void Setup()
        {
            applicationContext = new AttributeApplicationContext("Ndi.UnitTests.TestComponents.SimpleComponents");
        }

        [TestMethod]
        public void App_ShouldContainCorrectContext()
        {
            Assert.AreEqual(applicationContext, App.Instance.Context);
        }
    }
}
