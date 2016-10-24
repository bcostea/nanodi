using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ndi.UnitTests.TestComponents.InjectByType;
using Xunit;

namespace Ndi.UnitTests
{
    public class InjectByTypeNotNameTest
    {
        [Fact]
        public void Inject_Test()
        {

            IApplicationContext applicationContext = new AttributeApplicationContext("Ndi.UnitTests.TestComponents.InjectByType");
            Parent parent = (Parent) applicationContext.GetComponent("injectParentComponent");
            Assert.Equal("injectChildComponent", parent.ChildName);

        }
    }
}
