using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi;
using Xunit;
using Ndi.UnitTests.TestComponents.ConstructorInjection;

namespace Ndi.UnitTests
{
	
	public class AttributeApplicationContextTest:AbstractApplicationContextTest
	{

		public AttributeApplicationContextTest()
		{
            applicationContext = new AttributeApplicationContext("Ndi.UnitTests.TestComponents.SimpleComponents");
		}


		[Fact]
		public override void ApplicationContext_InitializeWithSource()
		{
            applicationContext.Destroy();

            applicationContext.Initialize("Ndi.UnitTests.TestComponents.SimpleComponents");
			applicationContext.Lifecycle.InitializedRequired();
		}

        [Fact]
        public void ApplicationContext_InitializeWithNoSource()
        {
            applicationContext.Destroy();
            applicationContext.Initialize();
            applicationContext.Lifecycle.InitializedRequired();
        }

        [Fact]
		public override void ApplicationContext_GetComponentCircularDependency()
		{
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.CircularDependencies");
		}

        [Fact]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.ConstructorInjection");

            Assert.NotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [Fact]
        public void ApplicationContext_ConstructorInjectionWorks_NoField()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.ConstructorInjection");

            Assert.NotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildWithNoField"));
        }

        [Fact]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.ConstructorInjection");

            Assert.NotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField"));
            Assert.NotNull(((ParentComponentWithConstructorThatRequiresChildAndOtherField)applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChildAndOtherField")).SecondChild);
        }

        [Fact]
        public void ApplicationContext_ComponentWithFieldRequiredInConstructor()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("Ndi.UnitTests.TestComponents.FieldRequiredInConstructor");
        }


        [Fact]
        public override void ApplicationContext_Destroy()
        {
            applicationContext.Destroy();
            applicationContext.Lifecycle.NotInitializedRequired();
        }
    }
}
