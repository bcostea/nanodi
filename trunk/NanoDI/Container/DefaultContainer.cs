#region Copyright 2009 Bogdan COSTEA
/** This File is part of the NanoDI Library
 *
 * Copyright 2009 Bogdan COSTEA
 * All rights reserved
 * 
 * This library is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published
 * by the Free Software Foundation; either version 2.1 of the License, or
 * (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this library; if not, write to the
 * Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor Boston, MA  02110-1301 USA
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Ndi.Attributes;
using Ndi.Component;
using Ndi.Component.ComponentActivator;
using Ndi.Component.Locator;
using Ndi.Component.Registry;
using Ndi.Component.Dependency;
using Ndi.Exceptions;
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
				IComponent component = componentRegistry.Get(componentName);
				return componentActivator.GetInstance(component);
		}

		public IMutableContainer AddComponent(string name, object component)
		{
			throw new NotImplementedException();
		}

		public Boolean HasComponent(string componentName)
		{
			return componentRegistry.Contains(componentName);
		}
	}
}
