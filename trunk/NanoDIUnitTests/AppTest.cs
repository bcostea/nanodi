using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi;
using NUnit.Framework;

namespace NdiUnitTests
{
    [TestFixture()]
    public class AppTest
    {
        private IApplicationContext applicationContext;

        [SetUp]
        public void Setup()
        {
            applicationContext = new AttributeApplicationContext("NdiUnitTests.TestComponents.SimpleComponents");
        }

        [Test]
        public void App_ShouldContainCorrectContext()
        {
            Assert.AreEqual(applicationContext, App.Instance.Context);
        }
    }
}
