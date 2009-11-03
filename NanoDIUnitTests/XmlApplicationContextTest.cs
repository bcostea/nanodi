using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NanoDI;

namespace NanoDIUnitTests
{
	[TestFixture()]
	class XmlApplicationContextTest:AbstractApplicationContextTest
	{
		[SetUp]
		public override void Setup()
		{
			applicationContext = new XmlApplicationContext();
		}

		[Test]
		public override void ApplicationContext_InitializeWithSource()
		{
			ApplicationContext_Destroy();

			applicationContext.Initialize("components.xml");
			applicationContext.Lifecycle.InitializedRequired();
		}

		[Test]
		public override void ApplicationContext_GetComponentCircularDependency()
		{
			ApplicationContext_Destroy();
			applicationContext.Initialize("circular.xml");
		}

	}
}
