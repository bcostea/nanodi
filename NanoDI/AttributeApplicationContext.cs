using System;
using NanoDI;
using NanoDI.Component.ComponentActivator;
using NanoDI.Component.Locator;
using NanoDI.Component.Registry;
using NanoDI.Container;
using NanoDI.Exceptions;
using NanoDI.Tooling.Logging;

namespace NanoDI
{
	public class AttributeApplicationContext : AbstractApplicationContext
	{
		static ILogger log = LogFactory.GetLog(typeof(AttributeApplicationContext));

		public AttributeApplicationContext(string targetNamespace)
			: base(targetNamespace)
		{
		}

		public override void Initialize(string targetNamespace)
		{
			if (log.IsDebugEnabled())
			{
				log.Debug("Initializing attribute application context.");
			}

			beforeInitialize();
			InitializeContainer(targetNamespace);
			afterInitialize();
		}

		public override void InitializeContainer(string targetNamespace)
		{
			container = new TreeContainer(new ReflectionLocator());

			if (targetNamespace == null)
			{
				container.Initialize();
			}
			else
			{
				container.Initialize(targetNamespace);
			}
		}
	}
}
