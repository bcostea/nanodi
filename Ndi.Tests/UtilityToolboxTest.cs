using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ndi;
using Ndi.Attributes;

namespace NdiUnitTests
{
    [TestFixture]
    public class UtilityToolboxTest
    {
        UtilityToolbox utilityToolbox;

        [SetUp()]
        public void SetUp()
        {
            utilityToolbox = new UtilityToolbox();
        }

        [Test]
        public void IsDebugEnabledTest()
        {
            utilityToolbox.IsDebugEnabled();
        }

		[Test]
		public void GetTypeTest()
		{
			Type type = UtilityToolbox.GetType("NdiUnitTests.UtilityToolboxTest");
			Assert.AreEqual(type, typeof(UtilityToolboxTest));
		}

		[Test]
		public void GetScope()
		{
			Scope scope = UtilityToolbox.GetScope("Prototype");
			Assert.AreEqual(scope, Scope.Prototype);
		}

		[Test]
		public void GetScopeFailed() 
		{
			Scope scope = UtilityToolbox.GetScope("InvalidScope");
			Assert.AreEqual(scope, Scope.Singleton);
		}

		[Test]
		public void GetScopeNullFailed()
		{
			Scope scope = UtilityToolbox.GetScope(null);
			Assert.AreEqual(scope, Scope.Singleton);
		}
    }
}
