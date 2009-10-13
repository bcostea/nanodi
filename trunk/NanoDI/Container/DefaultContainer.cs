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

using NanoDI.Attributes;
using NanoDI.Component;
using NanoDI.Component.ComponentActivator;
using NanoDI.Component.Locator;
using NanoDI.Component.Registry;
using NanoDI.Component.Dependency;
using NanoDI.Exceptions;

namespace NanoDI.Container
{
    class DefaultContainer : IMutableContainer
    {
        
        IComponentRegistry componentRegistry;
        ILocator componentLocator;
        IComponentActivator componentActivator;

        public DefaultContainer(IComponentRegistry componentRegistry, ILocator componentLocator, IComponentActivator componentActivator)
        {
            this.componentRegistry = componentRegistry;
            this.componentLocator = componentLocator;
            this.componentActivator = componentActivator;
        }

        public void Initialize()
        {
            componentRegistry.RegisterAll(componentLocator.Locate());
        }

        public object GetComponent(string componentName)
        {
            if (componentRegistry.Contains(componentName))
            {
            	IComponent component = componentRegistry.Get(componentName);
            	return componentActivator.GetInstance(component);
                
            }
            else
                throw new InvalidComponentException(componentName);
        }
    	
		public IMutableContainer AddComponent(string name, object component)
		{
			throw new NotImplementedException();
		}
    }
}
