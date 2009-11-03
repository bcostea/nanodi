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
using NanoDI.Exceptions;

namespace NanoDI.Component.Dependency
{
    class DependencyGraph
    {
        Boolean[][] dependencies;
        Dictionary<string, int> components = new Dictionary<string, int>();

        public DependencyGraph(int componentCount)
        {
            dependencies = new Boolean[componentCount][];
        }

        public void Clear()
        {
            dependencies = new Boolean[0][];
            components.Clear();
        }

        public void insertDependency(String dependent, String dependency)
        {
            int dependentIndex = getOrCreateIndex(dependent);
            int dependencyIndex = getOrCreateIndex(dependency);

            initDefaultDependency(dependentIndex);
            initDefaultDependency(dependencyIndex);

            markAsDependent(dependentIndex, dependencyIndex);

            validateNonCircularDependency(dependentIndex, dependencyIndex);
        }

        void initDefaultDependency(int index)
        {
            if (dependencies[index] == null)
                dependencies[index] = new Boolean[dependencies.Length];
        }

        void markAsDependent(int dependentIndex, int dependencyIndex)
        {
            if (dependencies[dependentIndex][dependencyIndex] == false)
            {
                dependencies[dependentIndex][dependencyIndex] = true;
            }
        }

        void validateNonCircularDependency(int componentIndex, int otherComponentIndex)
        {
            if (dependencyTest(componentIndex, otherComponentIndex)
                && dependencyTest(otherComponentIndex, componentIndex))
            {
                throw new CircularDependencyException(getComponentByIndex(componentIndex) + "," + getComponentByIndex(otherComponentIndex));
            }
        }

        Boolean dependencyTest(int dependentIndex, int dependencyIndex)
        {
            return dependencies[dependentIndex][dependencyIndex];
        }

        public List<string> GetDependencies(string componentName)
        {
            List<string> deps = new List<string>();

            if (!components.ContainsKey(componentName))
                return deps;

            int sourceComponentIdx = components[componentName];

            foreach(int i in components.Values)
            {
                if (dependencyTest(sourceComponentIdx, i))
                {
                    deps.Add(getComponentByIndex(i));
                }
            }

            return deps;
        }

        String getComponentByIndex(int i)
        {
            string key = (from k in components where int.Equals(k.Value, i) select k.Key).FirstOrDefault();
            return key;        
        }

        int getOrCreateIndex(string componentName)
        {
            int index;
            if (!components.ContainsKey(componentName))
            {
                index = components.Count();
                components.Add(componentName, index);
            }
            else
            {
                index = components[componentName];
            }
            return index;
        }

    }
}
