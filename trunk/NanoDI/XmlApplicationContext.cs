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
	public class XmlApplicationContext : AbstractApplicationContext
	{
		static ILogger log = LogFactory.GetLog(typeof(XmlApplicationContext));

		public XmlApplicationContext(): base()
		{
            Initialize();
		}

		public XmlApplicationContext(string fileName)
			: base(fileName)
		{
		}

		public override void Initialize(string fileName)
		{
			if (log.IsDebugEnabled())
			{
				log.Info("Initializing attribute application context.");
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

        public override void Initialize()
        {
           
        }
    }
}
