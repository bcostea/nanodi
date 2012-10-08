using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi.UnitTests.TestComponents.ConstructorInjection;

namespace Ndi.UnitTests
{
	[TestClass]
	public class AttributeApplicationContextTest:AbstractApplicationContextTest
	{

		[TestInitialize]
		public override void Setup()
		{
            applicationContext = new AttributeApplicationContext("Ndi.UnitTests.TestComponents.SimpleComponents");
		}


		[TestMethod]
		public override void ApplicationContext_InitializeWithSource()
		{
            applicationContext.Destroy();

            applicationContext.Initialize("Ndi.UnitTests.TestComponents.SimpleComponents");
			applicationContext.Lifecycle.InitializedRequired();
		}

        [TestMethod]
        public void ApplicationContext_InitializeWithNoSource()
        {
            applicationContext.Destroy();
            applicationContext.Initialize();
            applicationContext.Lifecycle.InitializedRequired();
        }

        [TestMethod]
		public override void ApplicationContext_GetComponentCircularDependency()
		{
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.CircularDependencies");
		}

        [TestMethod]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.ConstructorInjection");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [TestMethod]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.ConstructorInjection");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField"));
            Assert.IsNotNull(((ParentComponentWithConstructorThatRequiresChildAndOtherField)applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField")).SecondChild);
        }

        [TestMethod]
        public void ApplicationContext_ComponentWithFieldRequiredInConstructor()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.FieldRequiredInConstructor");
        }


        [TestMethod]
        public override void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
    }
}
