using System;
using Ndi;
using NUnit.Framework;
using NdiUnitTests.TestComponents;

namespace NdiUnitTests
{
    [TestFixture()]
    public class LifecycleTest
    {
        Lifecycle lifecycle;
		TestLifecycleObserver lifecycleObserver;
        [SetUp]
        public void Setup()
        {
            lifecycle = new Lifecycle();
			lifecycleObserver = new TestLifecycleObserver();
        }

        [Test]
        public void Lifecycle_NotInitialized()
        {
            lifecycle.NotInitializedRequired();
        }

        [Test]
        [ExpectedException("Ndi.Exceptions.CompositionException")]
        public void Lifecycle_NotInitializedFailed()
        {
            lifecycle.Initialized();
            lifecycle.NotInitializedRequired();
        }


        [Test]
        public void Lifecycle_Initialized()
        {
            lifecycle.Initialized();
        }

        [Test]
        public void Lifecycle_InitializedRequired()
        {
            lifecycle.Initialized();
            lifecycle.InitializedRequired();
        }


        [Test]
        [ExpectedException("Ndi.Exceptions.CompositionException")]
        public void Lifecycle_InitializedRequiredFailed()
        {
            lifecycle.InitializedRequired();
        }

		[Test]
		public void LifecycleRegisterObserver()
		{
			lifecycle.RegisterObserver(lifecycleObserver);
		}

		[Test]
		public void LifecycleObserverNone()
		{
			LifecycleRegisterObserver();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.Initial);
		}


		[Test]
		public void LifecycleObserverBeforeInit()
		{
			LifecycleRegisterObserver();
			lifecycle.BeforeInitialize();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.BeforeInit);
		}


		[Test]
		public void LifecycleObserverAfterInit()
		{
			LifecycleRegisterObserver();
			lifecycle.Initialized();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.AfterInit);
		}

		[Test]
		public void LifecycleObserverBeforeDestroyed()
		{
			LifecycleRegisterObserver();
			lifecycle.BeforeDestroy();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.BeforeDest);
		}

		[Test]
		public void LifecycleObserverAfterDestroyed()
		{
			LifecycleRegisterObserver();
			lifecycle.Destroyed();
			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.AfterDest);
		}

		[Test]
		public void LifecycleUnregisterObserver()
		{
			LifecycleRegisterObserver();
			lifecycle.UnregisterObserver(lifecycleObserver);

			lifecycle.Initialized();

			Assert.AreEqual(lifecycleObserver.State, TestLifecycleObserver.Initial);

		}


    }
}
