using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NanoDI;

namespace NanoDIUnitTests
{
	[TestFixture()]
	class AttributeApplicationContextTest:AbstractApplicationContextTest
	{
		[SetUp]
		public override void Setup()
		{
			applicationContext = new AttributeApplicationContext("NanoDIUnitTests.TestComponents.SimpleComponents");
		}

		[Test]
		public override void ApplicationContext_InitializeWithSource()
		{
			ApplicationContext_Destroy();

			applicationContext.Initialize("NanoDIUnitTests.TestComponents.SimpleComponents");
			applicationContext.Lifecycle.InitializedRequired();
		}

		[Test]
		public override void ApplicationContext_GetComponentCircularDependency()
		{
			ApplicationContext_Destroy();
			applicationContext.Initialize("NanoDIUnitTests.TestComponents.CircularDependencies");
		}


	}
}
