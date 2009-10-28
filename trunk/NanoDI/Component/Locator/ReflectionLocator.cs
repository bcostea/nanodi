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
            return LocateInNamespace("");
        }

        public List<IComponent> LocateInNamespace(string targetNamespace)
        {
            List<Type> types = new List<Type>();

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
                    ComponentAttribute component = attr as ComponentAttribute;
                    if (component != null)
                    {
                        if (log.IsDebugEnabled())
                            log.Debug("Component located " + component.Name);
                        components.Add(new Component(component.Name, type, component.Scope));

                    }
                }
            }
            return components;
        }
    }
}
