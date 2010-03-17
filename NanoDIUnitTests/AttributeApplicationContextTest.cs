using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ndi;
using NdiUnitTests.TestComponents.ConstructorInjection;

namespace NdiUnitTests
{
	[TestFixture()]
	class AttributeApplicationContextTest:AbstractApplicationContextTest
	{
		[SetUp]
		public override void Setup()
		{
			applicationContext = new AttributeApplicationContext("NdiUnitTests.TestComponents.SimpleComponents");
		}

		[Test]
		public override void ApplicationContext_InitializeWithSource()
		{
            applicationContext.Destroy();

			applicationContext.Initialize("NdiUnitTests.TestComponents.SimpleComponents");
			applicationContext.Lifecycle.InitializedRequired();
		}

        [Test]
        public void ApplicationContext_InitializeWithNoSource()
        {
            applicationContext.Destroy();
            applicationContext.Initialize();
            applicationContext.Lifecycle.InitializedRequired();
        }   

		[Test]
		public override void ApplicationContext_GetComponentCircularDependency()
		{
            applicationContext.Destroy();
			applicationContext.Initialize("NdiUnitTests.TestComponents.CircularDependencies");
		}

        [Test]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("NdiUnitTests.TestComponents.ConstructorInjection");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [Test]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("NdiUnitTests.TestComponents.ConstructorInjection");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField"));
            Assert.IsNotNull(((ParentComponentWithConstructorThatRequiresChildAndOtherField)applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField")).SecondChild);
        }


        [Test]
        public override void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
    }
}
