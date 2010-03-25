using NUnit.Framework;
using Ndi;
using NdiUnitTests.TestComponents.ConstructorInjection;

namespace NdiUnitTests
{
    [TestFixture()]
    public class XmlApplicationContextTest : AbstractApplicationContextTest
    {
        [SetUp]
        public override void Setup()
        {
            applicationContext = new XmlApplicationContext("components.xml");
        }

        [Test]
        public override void ApplicationContext_InitializeWithSource()
        {
            applicationContext.Lifecycle.InitializedRequired();
        }

        [Test]
        public override void ApplicationContext_GetComponentCircularDependency()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [Test]
        public void XmlApplicationContextWithFile()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [Test]
        [ExpectedException("Ndi.Exceptions.ComponentAlreadyExistsException")]
        public void ApplicationContextNameOverlap()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("nameoverlap.xml");
        }

        [Test]
        [ExpectedException("Ndi.Exceptions.CompositionException")]
        public void ApplicationContextInvalidType()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("invalidtype.xml");
        }

        [Test]
        [ExpectedException("Ndi.Exceptions.CompositionException")]
        public void ApplicationContextInvalidFieldForComponent()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("injectcomponentnofield.xml");
        }

        [Test]
        public override void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
    }
}
