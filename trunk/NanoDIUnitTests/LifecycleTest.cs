using System;
using NanoDI;
using NUnit.Framework;

namespace NanoDIUnitTests
{
    [TestFixture()]
    class LifecycleTest
    {
        Lifecycle lifecycle;

        [SetUp]
        public void Setup()
        {
            lifecycle = new Lifecycle();
        }

        [Test]
        public void Lifecycle_NotInitialized()
        {
            lifecycle.NotInitializedRequired();
        }

        [Test]
        [ExpectedException("NanoDI.Exceptions.CompositionException")]
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
        [ExpectedException("NanoDI.Exceptions.CompositionException")]
        public void Lifecycle_InitializedRequiredFailed()
        {
            lifecycle.InitializedRequired();
        }



    }
}
