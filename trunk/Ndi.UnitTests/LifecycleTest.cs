using System;
using Ndi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi.UnitTests.TestComponents;

namespace NdiUnitTests
{
    [TestClass]
    public class LifecycleTest
    {
        Lifecycle lifecycle;
		TestLifecycleObserver lifecycleObserver;

        [TestInitialize]
        public void Setup()
        {
            lifecycle = new Lifecycle();
			lifecycleObserver = new TestLifecycleObserver();
        }

        [TestMethod]
        public void Lifecycle_NotInitialized()
        {
            lifecycle.NotInitializedRequired();
        }

        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.CompositionException))]
        public void Lifecycle_NotInitializedFailed()
        {
            lifecycle.Initialized();
            lifecycle.NotInitializedRequired();
        }


        [TestMethod]
        public void Lifecycle_Initialized()
        {
            lifecycle.Initialized();
        }

        [TestMethod]
        public void Lifecycle_InitializedRequired()
        {
            lifecycle.Initialized();
            lifecycle.InitializedRequired();
        }


        [TestMethod]
        [ExpectedException(typeof(Ndi.Exceptions.CompositionException))]
        public void Lifecycle_InitializedRequiredFailed()
        {
            lifecycle.InitializedRequired();
        }

        [TestMethod]
		public void LifecycleRegisterObserver()
		{
			lifecycle.RegisterObserver(lifecycleObserver);
		}

        [TestMethod]
		public void LifecycleObserverNone()
		{
			LifecycleRegisterObserver();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.Initial);
		}


        [TestMethod]
		public void LifecycleObserverBeforeInit()
		{
			LifecycleRegisterObserver();
			lifecycle.BeforeInitialize();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.BeforeInit);
		}


        [TestMethod]
		public void LifecycleObserverAfterInit()
		{
			LifecycleRegisterObserver();
			lifecycle.Initialized();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.AfterInit);
		}

        [TestMethod]
		public void LifecycleObserverBeforeDestroyed()
		{
			LifecycleRegisterObserver();
			lifecycle.BeforeDestroy();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.BeforeDest);
		}

        [TestMethod]
		public void LifecycleObserverAfterDestroyed()
		{
			LifecycleRegisterObserver();
			lifecycle.Destroyed();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.AfterDest);
		}

        [TestMethod]
		public void LifecycleUnregisterObserver()
		{
			LifecycleRegisterObserver();
			lifecycle.UnregisterObserver(lifecycleObserver);

			lifecycle.Initialized();

			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.Initial);

		}


    }
}
