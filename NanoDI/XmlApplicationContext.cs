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
	public class XmlApplicationContext : AbstractApplicationContext
	{
		static ILogger log = LogFactory.GetLog(typeof(XmlApplicationContext));

		public XmlApplicationContext(): base()
		{
		}

		public XmlApplicationContext(string fileName)
			: base(fileName)
		{
		}

		public override void Initialize(string fileName)
		{
			if (log.IsDebugEnabled())
			{
				log.Debug("Initializing attribute application context.");
			}

			beforeInitialize();
			InitializeContainer(fileName);
			afterInitialize();
		}

		public override void InitializeContainer(string fileName)
		{
			container = new TreeContainer(new XmlLocator());

			if (fileName == null)
			{
				container.Initialize();
			}
			else
			{
				container.Initialize(fileName);
			}
		}
	}
}
