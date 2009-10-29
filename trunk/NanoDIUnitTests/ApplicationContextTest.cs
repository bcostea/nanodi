using System;
using NUnit.Framework;
using NanoDI;
using NanoDIUnitTests.TestComponents.SimpleComponents;

namespace NanoDIUnitTests
{

    [TestFixture()]
    class ApplicationContextTest
    {
        IParentComponent parentComponentOne;

        [SetUp]
        public void Setup()
        {
            ApplicationContext.Initialize("NanoDIUnitTests.TestComponents.SimpleComponents");
        }

        [Test]
        public void ApplicationContext_InitializeNamespace()
        {
            ApplicationContext.Destroy();
            ApplicationContext.Lifecycle.NotInitializedRequired();
            ApplicationContext.Initialize("NanoDIUnitTests.TestComponents.SimpleComponents");
            ApplicationContext.Lifecycle.InitializedRequired();
        }

        [Test]
        public void ApplicationContext_InitializeNoNamespace()
        {
            ApplicationContext.Destroy();
            ApplicationContext.Lifecycle.NotInitializedRequired();
            ApplicationContext.Initialize();
            ApplicationContext.Lifecycle.InitializedRequired();
        }

        [Test]
        [ExpectedException("NanoDI.Exceptions.CompositionException")]
        public void ApplicationContext_Destroy()
        {
            ApplicationContext.Destroy();
            ApplicationContext.GetComponent("parentComponentOne");
        }


        [Test]
        public void ApplicationContext_GetComponent()
        {
            parentComponentOne = (IParentComponent)ApplicationContext.GetComponent("parentComponentOne");
            Assert.IsNotNull(parentComponentOne);
        }

        [Test]
        [ExpectedException("NanoDI.Exceptions.InvalidComponentException")]
        public void ApplicationContext_GetComponentFailed()
        {
            parentComponentOne = (IParentComponent)ApplicationContext.GetComponent("magicalComponent");
        }


        [TearDown]
        public void Teardown()
        {
            ApplicationContext.Destroy();
        }
    }
}
