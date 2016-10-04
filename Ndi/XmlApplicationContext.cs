/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using Ndi.Component.Locator;
using Ndi.Container;
using Ndi.Tooling.Logging;

namespace Ndi
{
    /// <summary>
    /// An application context that is configured using an XML configuration file
    /// </summary>
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
