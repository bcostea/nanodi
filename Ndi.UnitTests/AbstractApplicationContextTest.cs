using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi.UnitTests.TestComponents.SimpleComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ndi.UnitTests
{
    public abstract class AbstractApplicationContextTest
    {
        protected IApplicationContext applicationContext;

        IParentComponent parentComponentOne;
        IChildComponent childComponentSingleton;
        IChildComponent childComponentPrototype;


        abstract public void Setup();

        public abstract void ApplicationContext_Destroy();
        public abstract void ApplicationContext_InitializeWithSource();
        public abstract void ApplicationContext_GetComponentCircularDependency();

        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.CompositionException))]
        public void ApplicationContext_GetComponentFailed()
        {
            applicationContext.Destroy();
            applicationContext.GetComponent("parentComponentOne");
        }


        [TestMethod]
        public void ApplicationContext_GetComponent()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("parentComponentOne");
            Assert.IsNotNull(parentComponentOne);
        }


        [TestMethod]
        public void ApplicationContext_GetSubComponent()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("parentComponentOne");
            Assert.IsNotNull(parentComponentOne.SingletonContent);
        }


        [TestMethod]
        public void ApplicationContext_GetComponentSingleton()
        {
            childComponentSingleton = (IChildComponent)applicationContext.GetComponent("singletonComponent");
            Assert.IsNotNull(childComponentSingleton);
        }

        [TestMethod]
        public void ApplicationContext_GetComponentSingletonIsSingleton()
        {
            childComponentSingleton = (IChildComponent)applicationContext.GetComponent("singletonComponent");
            IChildComponent sameObject = (IChildComponent)applicationContext.GetComponent("singletonComponent");

            Assert.AreSame(childComponentSingleton, sameObject);
        }

        [TestMethod]
        public void ApplicationContext_GetComponentProtoype()
        {
            childComponentPrototype = (IChildComponent)applicationContext.GetComponent("prototypeComponent");
            Assert.IsNotNull(childComponentPrototype);
        }

        [TestMethod]
        public void ApplicationContext_GetComponentProtoypeIsPrototype()
        {
            childComponentPrototype = (IChildComponent)applicationContext.GetComponent("prototypeComponent");
            IChildComponent otherObject = (IChildComponent)applicationContext.GetComponent("prototypeComponent");

            Assert.AreNotSame(childComponentPrototype, otherObject);
        }

        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.InvalidComponentException))]
        public void ApplicationContext_GetComponentInvalid()
        {
            parentComponentOne = (IParentComponent)applicationContext.GetComponent("invalidComponent");
        }

        [TestCleanup]
        public void Teardown()
        {
            applicationContext.Destroy();
        }
    }
}
