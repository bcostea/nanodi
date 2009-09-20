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
using System.Collections;
using System.Linq;
using System.Reflection;
using NanoDI.Exceptions;
using NanoDI.Dependency;

namespace NanoDI.Container
{
    class DefaultContainer : IMutableContainer
    {
        Dictionary<string, object> components = new Dictionary<string, object>();
        DependencyManager dependencyManager = new DependencyManager();
            
        public void initialize()
        {
            dependencyManager.buildDependencyGraph();
            initializeComponents();
        }

        private void initializeComponents()
        {
            Dictionary<string, string> declaredComponents = dependencyManager.DeclaredComponents;
            foreach (string componentName in declaredComponents.Keys)
            {
                instantiateComponent(componentName);
            }
        }

        // a little dirty, needs some refactoring and love
        object instantiateComponent(string componentName)
        {
            if (components.Keys.Contains(componentName) && components[componentName] != null)
                return components[componentName];

            Dictionary<string, string> declaredComponents = dependencyManager.DeclaredComponents;
            string componentConcreteClassName = (string) declaredComponents[componentName];
            Type actualComponentType = Type.GetType(componentConcreteClassName);
            var actualComponent = Activator.CreateInstance(actualComponentType);

            if (componentConcreteClassName != null)
            {
                List<string> dependencies = dependencyManager.getDependencies(componentConcreteClassName);
                
                if (dependencies.Count > 0)
                {
                    foreach (string dependency in dependencies)
                    {
                        string dependentComponentName = (from cmp in declaredComponents where int.Equals(cmp.Value, dependency) select cmp.Key).FirstOrDefault();
                        FieldInfo fieldInfo = actualComponentType.GetField(dependentComponentName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        if (fieldInfo != null)
                        {
                            object createdDependency = instantiateComponent(dependentComponentName);
                            fieldInfo.SetValue(actualComponent, createdDependency);
                        }
                        else
                            throw new CompositionException();
                    }
                }
                components.Add(componentName, actualComponent);

                return actualComponent;
            }
            else
                throw new InvalidComponentException(componentName);
        }

        public IMutableContainer addComponent(string name, object component){
            if (!components.ContainsKey(name))
                components.Add(name, component);
            else
                throw new ComponentAlreadyExistsException(name);

            return this;
        }

        public object getComponent(string name)
        {
            if(components.ContainsKey(name))
                return components[name];
            else
                throw new InvalidComponentException(name);
        }

        public List<object> getComponents()
        {
            return new List<object>();
        }

    }
}
