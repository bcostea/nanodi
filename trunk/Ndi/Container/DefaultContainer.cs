/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using Ndi.Component;
using Ndi.Component.ComponentActivator;
using Ndi.Component.Locator;
using Ndi.Component.Registry;
using Ndi.Component.Cache;

namespace Ndi.Container
{
	class DefaultContainer : IMutableContainer
	{

		IComponentRegistry componentRegistry;
		ILocator componentLocator;
		IComponentActivator componentActivator;
		IComponentCache componentCache;

		public DefaultContainer(ILocator componentLocator)
		{
			this.componentLocator = componentLocator;

			this.componentRegistry = new DefaultComponentRegistry();
			this.componentCache = new DefaultComponentCache();
			this.componentActivator = new DefaultComponentActivator(this.componentRegistry, this.componentCache);
		}

		public void Initialize()
		{
			componentRegistry.RegisterAll(componentLocator.Locate());
		}

		public void Initialize(string componentSource)
		{
			componentRegistry.RegisterAll(componentLocator.Locate(componentSource));
		}

		public void Destroy()
		{
			componentRegistry.UnregisterAll();
			componentCache.Clear();
		}

		public object GetComponent(string componentName)
		{
				IComponent component = componentRegistry.GetComponent(componentName);
				return componentActivator.GetInstance(component);
		}

		public IMutableContainer AddComponent(string name, object component)
		{
			throw new NotImplementedException();
		}

		public Boolean HasComponent(string componentName)
		{
			return componentRegistry.ContainsComponent(componentName);
		}
	}
}
