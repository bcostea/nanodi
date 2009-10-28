using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NanoDIUnitTests.TestComponents.SimpleComponents;



namespace NanoDI.UnitTests
{
    [TestFixture()]
    public class ApplicationContextTests
    {
        IParentComponent parentComponentOne;

        [SetUp]
        public void Setup()
        {
            ApplicationContext.Initialize("NanoDIUnitTests.TestComponents.SimpleComponents");
        }

        [Test]
        public void ApplicationContext_GetComponent()
        {
            parentComponentOne = (IParentComponent) ApplicationContext.GetComponent("parentComponentOne");
            Assert.IsNotNull(parentComponentOne);
        }


    }
}
