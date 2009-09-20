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
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NanoDI.Attributes;

namespace NanoDI.Dependency
{
    class DependencyManager
    {
        Dictionary<string, string> componentTypes = new Dictionary<string, string>();
        DependencyGraph dependencyGraph;

        public Dictionary<string, string> DeclaredComponents{get{return this.componentTypes;}}

        public void buildDependencyGraph()
        {
            findAllDeclaredComponents();
            foreach (string componentFullClassName in componentTypes.Values)
            {
                Type componentType = Type.GetType(componentFullClassName);
                foreach (FieldInfo fieldInfo in componentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
                    {
                        InjectAttribute inject = attr as InjectAttribute;
                        if (inject != null)
                        {
                            foreach (string fullClassName in componentTypes.Values)
                            {
                                foreach (Type iface in getImplementedInterfaces(fullClassName))
                                {
                                    if(iface.FullName.Equals(fieldInfo.FieldType.FullName))
                                        dependencyGraph.insertDependency(new Dependency(componentFullClassName, fullClassName));
                                   
                                }
                            }                           
                        }
                    }
                }
            }
        }

        public List<string> getDependencies(string componentName)
        {
            return dependencyGraph.getDependencies(componentName);
        }

        private void findAllDeclaredComponents()
        {
            Assembly asm = Assembly.GetEntryAssembly();

            foreach (Type type in asm.GetTypes())
            {
                foreach (Attribute attr in type.GetCustomAttributes(true))
                {
                    ComponentAttribute component = attr as ComponentAttribute;
                    if (component != null)
                    {
                        componentTypes.Add(component.Name, type.AssemblyQualifiedName);
                    }
                }
            }
            dependencyGraph = new DependencyGraph(componentTypes.Count);
        }

        private static Type[] getImplementedInterfaces(string componentFullClassName)
        {
            Type componentType = Type.GetType(componentFullClassName);
            return componentType.GetInterfaces();
        }
    }
}
