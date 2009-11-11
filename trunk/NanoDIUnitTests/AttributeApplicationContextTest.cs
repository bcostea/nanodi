using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ndi;

namespace NdiUnitTests
{
	[TestFixture()]
	class AttributeApplicationContextTest:AbstractApplicationContextTest
	{
		[SetUp]
		public override void Setup()
		{
			applicationContext = new AttributeApplicationContext("NdiUnitTests.TestComponents.SimpleComponents");
		}

		[Test]
		public override void ApplicationContext_InitializeWithSource()
		{
			ApplicationContext_Destroy();

			applicationContext.Initialize("NdiUnitTests.TestComponents.SimpleComponents");
			applicationContext.Lifecycle.InitializedRequired();
		}

		[Test]
		public override void ApplicationContext_GetComponentCircularDependency()
		{
			ApplicationContext_Destroy();
			applicationContext.Initialize("NdiUnitTests.TestComponents.CircularDependencies");
		}


	}
}
