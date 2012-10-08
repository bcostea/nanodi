using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ndi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ndi.UnitTests
{
    [TestClass]
    public class UtilityToolboxTest
    {
        UtilityToolbox utilityToolbox;

        [TestInitialize]
        public void SetUp()
        {
            utilityToolbox = new UtilityToolbox();
        }

        [TestMethod]
        public void IsDebugEnabledTest()
        {
            utilityToolbox.IsDebugEnabled();
        }

        [TestMethod]
        public void GetTypeTest()
        {
            Type type = UtilityToolbox.GetType("Ndi.UnitTests.UtilityToolboxTest");
            Assert.AreEqual(type, typeof(UtilityToolboxTest));
        }

        [TestMethod]
        public void GetScope()
        {
            Scope scope = UtilityToolbox.GetScope("Prototype");
            Assert.AreEqual(scope, Scope.Prototype);
        }

        [TestMethod]
        public void GetScopeFailed()
        {
            Scope scope = UtilityToolbox.GetScope("InvalidScope");
            Assert.AreEqual(scope, Scope.Singleton);
        }

        [TestMethod]
        public void GetScopeNullFailed()
        {
            Scope scope = UtilityToolbox.GetScope(null);
            Assert.AreEqual(scope, Scope.Singleton);
        }
    }
}
