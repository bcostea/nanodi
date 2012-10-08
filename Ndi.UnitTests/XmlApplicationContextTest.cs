using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi;

namespace Ndi.UnitTests
{
    [TestClass]
    public class XmlApplicationContextTest : AbstractApplicationContextTest
    {
        [TestInitialize]
        public override void Setup()
        {
            applicationContext = new XmlApplicationContext("components.xml");
        }

        [TestMethod]
        public override void ApplicationContext_InitializeWithSource()
        {
            applicationContext.Lifecycle.InitializedRequired();
        }

        [TestMethod]
        public override void ApplicationContext_GetComponentCircularDependency()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [TestMethod]
        public void XmlApplicationContextWithFile()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.ComponentAlreadyExistsException))]
        public void ApplicationContextNameOverlap()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("nameoverlap.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.CompositionException))]
        public void ApplicationContextInvalidType()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("invalidtype.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.CompositionException))]
        public void ApplicationContextInvalidFieldForComponent()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("injectcomponentnofield.xml");
        }

        [TestMethod]
        public override void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
    }
}
