using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ndi;

namespace NdiUnitTests
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

		[Test]
		public void XmlApplicationContextWithFile()
		{
			ApplicationContext_Destroy();
			applicationContext = new XmlApplicationContext("circular.xml");
		}

		[Test]
		[ExpectedException("Ndi.Exceptions.ComponentAlreadyExistsException")]
		public void ApplicationContextNameOverlap()
		{
			ApplicationContext_Destroy();
			applicationContext = new XmlApplicationContext("nameoverlap.xml");
		}

		[Test]
		[ExpectedException("Ndi.Exceptions.CompositionException")]
		public void ApplicationContextInvalidType()
		{
			ApplicationContext_Destroy();
			applicationContext = new XmlApplicationContext("invalidtype.xml");
		}


	}
}
