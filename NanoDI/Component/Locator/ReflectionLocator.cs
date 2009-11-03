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
using NanoDI.Attributes;
using System.Reflection;
using System.Linq;
using NanoDI.Tooling.Logging;

namespace NanoDI.Component.Locator
{
    class ReflectionLocator : ILocator
    {
        ILogger log = LogFactory.GetLog(typeof(ReflectionLocator));

        public List<IComponent> Locate()
        {
            return Locate("");
        }

        public List<IComponent> Locate(string targetNamespace)
        {
            List<Type> types = new List<Type>();

			if (log.IsDebugEnabled())
			{
				log.Debug("Locating components defined in namespace \"" + targetNamespace + "\"");
			}

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                IEnumerable<Type> asmTypes = from t in asm.GetTypes()
                                             where t.IsClass &&
                                                 (t.Namespace != null && t.Namespace.StartsWith(targetNamespace))
                                             select t;
                types.AddRange(asmTypes);
            }

            return LocateInTypes(types.ToArray());
        }

        public List<IComponent> LocateInTypes(Type[] types)
        {
            List<IComponent> components = new List<IComponent>();

            foreach (Type type in types)
            {
                foreach (Attribute attr in type.GetCustomAttributes(true))
                {
                    ComponentAttribute componentAttr = attr as ComponentAttribute;
					if (componentAttr != null)
                    {
                        if (log.IsDebugEnabled())
							log.Debug("Component located " + componentAttr.Name);

						Component component = new Component(componentAttr.Name, type, componentAttr.Scope);
						component.Fields = getInjectableFields(type);
                        components.Add(component);

                    }
                }
            }
            return components;
        }

		List<ComponentField> getInjectableFields(Type type)
		{
			List<ComponentField> fields = new List<ComponentField>();

			foreach (FieldInfo possibleDependencyField in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
			{
				if(fieldIsInjectable(possibleDependencyField))
				{
					fields.Add(new ComponentField(possibleDependencyField.Name));
				}

			}

			return fields;
		}

		Boolean fieldIsInjectable(FieldInfo fieldInfo)
		{
			foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
			{
				InjectAttribute inject = attr as InjectAttribute;
				if (inject != null)
				{
					return true;
				}
			}
			return false;
		}

    }
}
