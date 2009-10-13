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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NanoDI.Exceptions;
using NanoDI.Attributes;
using NanoDI.Component.Dependency;
using NanoDI.Container;
using NanoDI.Component.ComponentActivator;

namespace NanoDI.Component.Registry
{
    class DefaultComponentRegistry : IComponentRegistry
    {
        Dictionary<string, IComponent> components = new Dictionary<string, IComponent>();
        DependencyGraph dependencyGraph;

        public void RegisterAll(List<IComponent> components)
        {
        	if (dependencyGraph==null)
        		dependencyGraph = new DependencyGraph(components.Count);
        	
        	foreach(IComponent component in components)
        	{
                addComponent(component.Name, component);
                
                foreach (FieldInfo fieldInfo in component.Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    addComponentDependencyIfInjectable(component, fieldInfo);
                }
        	}
        }


        void addComponent(string name, IComponent component)
        {
            if (!components.ContainsKey(name))
                components.Add(name, component);
            else
                throw new ComponentAlreadyExistsException(name);
        }

        void addComponentDependencyIfInjectable(IComponent parentComponent, FieldInfo fieldInfo)
        {
            foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
            {
                InjectAttribute inject = attr as InjectAttribute;
                if (inject != null)
                {
                    foreach (IComponent component in components.Values)
                    {
                        foreach (Type iFace in getImplementedInterfaces(component.Type))
                        {
                            if (iFace.FullName.Equals(fieldInfo.FieldType.FullName))
                                dependencyGraph.insertDependency(parentComponent.Name, component.Name);
                        }
                    }
                }
            }
        }

        public List<IComponent> GetComponentDependencies(IComponent component)
        {
            List<IComponent> dependencies = new List<IComponent>();
            List<string> stringDependencies = dependencyGraph.getDependencies(component.Name);

            foreach (string dep in stringDependencies)
            {
                if (Contains(dep))
                {
                    dependencies.Add(Get(dep));
                }
                else
                    throw new CompositionException();
            }

            return dependencies;
        }

       
        private static Type[] getImplementedInterfaces(Type componentType)
        {
            return componentType.GetInterfaces();
        }

		public bool Contains(string componentName)
		{
			return components.ContainsKey(componentName);
		}
    	
		public IComponent Get(string componentName)
		{
			if(Contains(componentName))
				return components[componentName];
			else
				throw new InvalidComponentException(componentName);
		}
    	
		public Type GetType(string componentName)
		{
			return Get(componentName).GetType();
		}
    	
    }
}
