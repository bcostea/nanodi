using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NanoDI;

namespace NanoDIUnitTests
{
    [TestFixture]
    class UtilityToolboxTest
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
    }
}
