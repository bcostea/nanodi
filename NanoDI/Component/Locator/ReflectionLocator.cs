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
                IEnumerable<Type> asmTypes = from type in asm.GetTypes()
                                             where type.IsClass &&
                                                 (type.Namespace != null && type.Namespace.StartsWith(targetNamespace))
                                             select type;
                types.AddRange(asmTypes);
            }

            return LocateInTypes(types.ToArray());
        }

        public List<IComponent> LocateInTypes(Type[] types)
        {
           
			IEnumerable<Component> components = 
				from type in types
					where type.GetCustomAttributes(typeof(ComponentAttribute), true).Length > 0
						from attribute in type.GetCustomAttributes(typeof(ComponentAttribute), true)
					select new Component(((ComponentAttribute)attribute).Name,
										 type, 
										 ((ComponentAttribute)attribute).Scope, 
										 getInjectableFields(type));

			return components.ToList().ConvertAll(new Converter<Component, IComponent>(component => component));      

		}

		List<ComponentField> getInjectableFields(Type type)
		{
			IEnumerable<ComponentField> fields = 
				from possibleDependencyField in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
					where fieldIsInjectable(possibleDependencyField)
					select new ComponentField(possibleDependencyField.Name);

			return fields.ToList();
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
