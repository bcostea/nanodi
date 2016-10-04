using System;
using Ndi;
using Ndi.Exceptions;
using Xunit;
using Ndi.UnitTests.TestComponents;

namespace NdiUnitTests
{
    
    public class LifecycleTest
    {
        Lifecycle lifecycle;
		TestLifecycleObserver lifecycleObserver;

        public LifecycleTest()
        {
            lifecycle = new Lifecycle();
			lifecycleObserver = new TestLifecycleObserver();
        }

        [Fact]
        public void Lifecycle_NotInitialized()
        {
            lifecycle.NotInitializedRequired();
        }

        [Fact]
        public void Lifecycle_NotInitializedFailed()
        {
            lifecycle.Initialized();
            Assert.Throws<CompositionException>(()=>lifecycle.NotInitializedRequired());
        }


        [Fact]
        public void Lifecycle_Initialized()
        {
            lifecycle.Initialized();
        }

        [Fact]
        public void Lifecycle_InitializedRequired()
        {
            lifecycle.Initialized();
            lifecycle.InitializedRequired();
        }


        [Fact]
        public void Lifecycle_InitializedRequiredFailed()
        {
            Assert.Throws<CompositionException>(()=>lifecycle.InitializedRequired());
        }

        [Fact]
		public void LifecycleRegisterObserver()
		{
			lifecycle.RegisterObserver(lifecycleObserver);
		}

        [Fact]
		public void LifecycleObserverNone()
		{
			LifecycleRegisterObserver();
			Assert.Equal(lifecycleObserver.State, TestLifecycleObserver.Initial);
		}


        [Fact]
		public void LifecycleObserverBeforeInit()
		{
			LifecycleRegisterObserver();
			lifecycle.BeforeInitialize();
			Assert.Equal(lifecycleObserver.State, TestLifecycleObserver.BeforeInit);
		}


        [Fact]
		public void LifecycleObserverAfterInit()
		{
			LifecycleRegisterObserver();
			lifecycle.Initialized();
			Assert.Equal(lifecycleObserver.State, TestLifecycleObserver.AfterInit);
		}

        [Fact]
		public void LifecycleObserverBeforeDestroyed()
		{
			LifecycleRegisterObserver();
			lifecycle.BeforeDestroy();
			Assert.Equal(lifecycleObserver.State, TestLifecycleObserver.BeforeDest);
		}

        [Fact]
		public void LifecycleObserverAfterDestroyed()
		{
			LifecycleRegisterObserver();
			lifecycle.Destroyed();
			Assert.Equal(lifecycleObserver.State, TestLifecycleObserver.AfterDest);
		}

        [Fact]
		public void LifecycleUnregisterObserver()
		{
			LifecycleRegisterObserver();
			lifecycle.UnregisterObserver(lifecycleObserver);

			lifecycle.Initialized();

			Assert.Equal(lifecycleObserver.State, TestLifecycleObserver.Initial);

		}


    }
}
