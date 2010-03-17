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
    class XmlApplicationContextTest : AbstractApplicationContextTest
    {
        [SetUp]
        public override void Setup()
        {
            applicationContext = new XmlApplicationContext("components.xml");
        }

        [Test]
        public override void ApplicationContext_InitializeWithSource()
        {
            applicationContext.Lifecycle.InitializedRequired();
        }

        [Test]
        public override void ApplicationContext_GetComponentCircularDependency()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [Test]
        public void XmlApplicationContextWithFile()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("circular.xml");
        }

        [Test]
        [ExpectedException("Ndi.Exceptions.ComponentAlreadyExistsException")]
        public void ApplicationContextNameOverlap()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("nameoverlap.xml");
        }

        [Test]
        [ExpectedException("Ndi.Exceptions.CompositionException")]
        public void ApplicationContextInvalidType()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("invalidtype.xml");
        }

        [Test]
        public void ApplicationContext_ConstructorInjectionWorks()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("constructorinjection.xml");

            Assert.IsNotNull(applicationContext.GetComponent("parentComponentWithConstructorThatRequiresChild"));
        }

        [Test]
        public void ApplicationContext_ConstructorInjectionWorksWithAdditionalFields()
        {
            applicationContext.Destroy();
            applicationContext.Initialize("constructorinjection.xml");

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
