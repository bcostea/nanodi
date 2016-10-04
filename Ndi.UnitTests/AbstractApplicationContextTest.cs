using System;
using Xunit;
using Ndi.UnitTests.TestComponents.SimpleComponents;
using Ndi.Exceptions;
using Assert = Xunit.Assert;

namespace Ndi.UnitTests
{
    public abstract class AbstractApplicationContextTest : IDisposable
    {
        protected IApplicationContext applicationContext;

        IParentComponent parentComponentOne;
        IChildComponent childComponentSingleton;
        IChildComponent childComponentPrototype;


        public abstract void ApplicationContext_Destroy();
        public abstract void ApplicationContext_InitializeWithSource();
        public abstract void ApplicationContext_GetComponentCircularDependency();

        [Fact]
        public void ApplicationContext_GetComponentFailed()
        {
            applicationContext.Destroy();
            Assert.Throws<CompositionException>(()=> applicationContext.GetComponent("parentComponentOne"));
        }


        [Fact]
        public void ApplicationContext_GetComponent()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("parentComponentOne");
            Assert.NotNull(parentComponentOne);
        }


        [Fact]
        public void ApplicationContext_GetSubComponent()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("parentComponentOne");
            Assert.NotNull(parentComponentOne.SingletonContent);
        }


        [Fact]
        public void ApplicationContext_GetComponentSingleton()
        {
            childComponentSingleton = (IChildComponent)applicationContext.GetComponent("singletonComponent");
            Assert.NotNull(childComponentSingleton);
        }

        [Fact]
        public void ApplicationContext_GetComponentSingletonIsSingleton()
        {
            childComponentSingleton = (IChildComponent)applicationContext.GetComponent("singletonComponent");
            IChildComponent sameObject = (IChildComponent)applicationContext.GetComponent("singletonComponent");

            Assert.Same(childComponentSingleton, sameObject);
        }

        [Fact]
        public void ApplicationContext_GetComponentProtoype()
        {
            childComponentPrototype = (IChildComponent)applicationContext.GetComponent("prototypeComponent");
            Assert.NotNull(childComponentPrototype);
        }

        [Fact]
        public void ApplicationContext_GetComponentProtoypeIsPrototype()
        {
            childComponentPrototype = (IChildComponent)applicationContext.GetComponent("prototypeComponent");
            IChildComponent otherObject = (IChildComponent)applicationContext.GetComponent("prototypeComponent");

            Assert.NotSame(childComponentPrototype, otherObject);
        }

        [Fact]
        public void ApplicationContext_GetComponentInvalid()
        {
            Assert.Throws<InvalidComponentException>(() => applicationContext.GetComponent("invalidComponent"));

            ;
        }

        public void Dispose()
        {
            applicationContext.Destroy();
        }
    }
}
