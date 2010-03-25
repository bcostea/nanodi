using System;
using Ndi;
using Ndi.Component.ComponentActivator;
using Ndi.Component.Locator;
using Ndi.Component.Registry;
using Ndi.Container;
using Ndi.Exceptions;
using Ndi.Tooling.Logging;

namespace Ndi
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
