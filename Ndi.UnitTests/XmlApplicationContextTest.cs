using Xunit;
using Ndi;
using Ndi.Exceptions;

namespace Ndi.UnitTests
{
    
    public class XmlApplicationContextTest : AbstractApplicationContextTest
    {
        public  XmlApplicationContextTest()
        {
            applicationContext = new XmlApplicationContext("components.xml");
        }

        [Fact]
        public override void ApplicationContext_InitializeWithSource()
        {
            applicationContext.Lifecycle.InitializedRequired();
        }

        [Fact]
        public override void ApplicationContext_GetComponentCircularDependency()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [Fact]
        public void XmlApplicationContextWithFile()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [Fact]
        public void ApplicationContextNameOverlap()
        {
            applicationContext.Destroy();
            Assert.Throws<ComponentAlreadyExistsException>(() => applicationContext.Initialize("nameoverlap.xml"));

        }

        [Fact]
        public void ApplicationContextInvalidType()
        {
            applicationContext.Destroy();
            Assert.Throws<CompositionException>(() => applicationContext.Initialize("invalidtype.xml"));

        }

        [Fact]
        public void ApplicationContextInvalidFieldForComponent()
        {
            applicationContext.Destroy();
            Assert.Throws<CompositionException>(()=>applicationContext.Initialize("injectcomponentnofield.xml"));
        }

        [Fact]
        public override void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
    }
}
