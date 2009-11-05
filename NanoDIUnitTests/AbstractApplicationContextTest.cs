using System;
using NUnit.Framework;
using NanoDI;
using NanoDIUnitTests.TestComponents.SimpleComponents;
using NanoDIUnitTests.TestComponents.CircularDependencies;

namespace NanoDIUnitTests
{


    abstract class AbstractApplicationContextTest
    {
        protected IApplicationContext applicationContext;

        IParentComponent parentComponentOne;
        IChildComponent childComponentSingleton;
        IChildComponent childComponentPrototype;


		abstract public void Setup();

		public abstract void ApplicationContext_InitializeWithSource();
		public abstract void ApplicationContext_GetComponentCircularDependency();

        [Test]
        public void ApplicationContext_InitializeWithNoSource()
        {
            ApplicationContext_Destroy();

            applicationContext.Initialize();
            applicationContext.Lifecycle.InitializedRequired();
        }

        [Test]
        public void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
        

        [Test]
        [ExpectedException("NanoDI.Exceptions.CompositionException")]
        public void ApplicationContext_GetComponentFailed()
        {
            applicationContext.Destroy();
            applicationContext.GetComponent("parentComponentOne");
        }


        [Test]
        public void ApplicationContext_GetComponent()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("parentComponentOne");
            Assert.IsNotNull(parentComponentOne);
        }


        [Test]
        public void ApplicationContext_GetSubComponent()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("parentComponentOne");
            Assert.IsNotNull(parentComponentOne.SingletonContent);
        }


        [Test]
        public void ApplicationContext_GetComponentSingleton()
        {
            childComponentSingleton = (IChildComponent)applicationContext.GetComponent("singletonComponent");
            Assert.IsNotNull(childComponentSingleton);
        }

        [Test]
        public void ApplicationContext_GetComponentSingletonIsSingleton()
        {
            childComponentSingleton = (IChildComponent)applicationContext.GetComponent("singletonComponent");
            IChildComponent sameObject = (IChildComponent)applicationContext.GetComponent("singletonComponent");

            Assert.AreSame(childComponentSingleton, sameObject);
        }

        [Test]
        public void ApplicationContext_GetComponentProtoype()
        {
            childComponentPrototype = (IChildComponent)applicationContext.GetComponent("prototypeComponent");
            Assert.IsNotNull(childComponentPrototype);
        }

        [Test]
        public void ApplicationContext_GetComponentProtoypeIsPrototype()
        {
            childComponentPrototype = (IChildComponent)applicationContext.GetComponent("prototypeComponent");
            IChildComponent otherObject = (IChildComponent)applicationContext.GetComponent("prototypeComponent");

            Assert.AreNotSame(childComponentPrototype, otherObject);
        }

        [Test]
        [ExpectedException("NanoDI.Exceptions.InvalidComponentException")]
        public void ApplicationContext_GetComponentInvalid()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("invalidComponent");
        }

		[TearDown]
        public void Teardown()
        {
            applicationContext.Destroy();
        }
    }
}
