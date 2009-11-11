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
using Ndi.Attributes;
using Ndi.Component.Registry;
using System.Collections.Generic;
using System.Reflection;
using Ndi.Exceptions;
using Ndi.Component.Cache;

namespace Ndi.Component.ComponentActivator
{
	class DefaultComponentActivator : IComponentActivator
	{
        private IComponentRegistry componentRegistry;
        private IComponentCache componentCache;

        public DefaultComponentActivator(IComponentRegistry componentRegistry, IComponentCache componentCache)
        {
            this.componentRegistry = componentRegistry;
            this.componentCache = componentCache;
        }

		public object GetInstance(IComponent component)
		{
            if (Scope.Singleton.Equals(component.Scope) && componentCache.Contains(component))
                return componentCache.Get(component);

            return instantiateComponent(component);
      
		}

        object instantiateComponent(IComponent component)
        {

            Type actualComponentType = component.Type;
            var actualComponentInstance = Activator.CreateInstance(actualComponentType);

            if (actualComponentType.AssemblyQualifiedName != null)
            {
                List<IComponent> dependencies = componentRegistry.GetComponentDependencies(component);

                if (dependencies.Count > 0)
                {
                    foreach (IComponent dependentComponent in dependencies)
                    {
                        FieldInfo fieldInfo = actualComponentType.GetField(dependentComponent.Name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        if (fieldInfo != null)
                        {
                            object createdDependency = instantiateComponent(dependentComponent);
                            fieldInfo.SetValue(actualComponentInstance, createdDependency);
                        }
                        else
                            throw new CompositionException();
                    }
                }

                if (Scope.Singleton.Equals(component.Scope))
                {
                    componentCache.Put(component, actualComponentInstance);
                }

                return actualComponentInstance;
            }
            else
                throw new InvalidComponentException(component.Name);
        }
	}


}
