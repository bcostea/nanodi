using Xunit;
using Ndi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ndi.UnitTests
{
    
    public class UtilityToolboxTest
    {
        UtilityToolbox utilityToolbox;

        public UtilityToolboxTest()
        {
            utilityToolbox = new UtilityToolbox();
        }

        [Fact]
        public void IsDebugEnabledTest()
        {
            utilityToolbox.IsDebugEnabled();
        }

        [Fact]
        public void GetTypeTest()
        {
            Type type = UtilityToolbox.GetType("Ndi.UnitTests.UtilityToolboxTest");
            Assert.Equal(type, typeof(UtilityToolboxTest));
        }

        [Fact]
        public void GetScope()
        {
            Scope scope = UtilityToolbox.GetScope("Prototype");
            Assert.Equal(scope, Scope.Prototype);
        }

        [Fact]
        public void GetScopeFailed()
        {
            Scope scope = UtilityToolbox.GetScope("InvalidScope");
            Assert.Equal(scope, Scope.Singleton);
        }

        [Fact]
        public void GetScopeNullFailed()
        {
            Scope scope = UtilityToolbox.GetScope(null);
            Assert.Equal(scope, Scope.Singleton);
        }
    }
}
