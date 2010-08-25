/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
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
    /// An application context that is configured using Attributes
    /// </summary>
	public class AttributeApplicationContext : AbstractApplicationContext
	{
		static ILogger log = LogFactory.GetLog(typeof(AttributeApplicationContext));

        public AttributeApplicationContext()
            : base()
        {
            Initialize();
        }

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

        public override void Initialize()
        {
            beforeInitialize();
            InitializeContainer(null);
            afterInitialize();
        }
    }
}
