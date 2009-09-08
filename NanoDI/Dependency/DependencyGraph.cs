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

namespace NanoDI.Dependency
{
    class DependencyGraph
    {
        Boolean[,] dependencies;
        Dictionary<string, int> componentIndex = new Dictionary<string, int>();
        int lastDependencyIndex=0;
        int DependencyCount=0;

        public DependencyGraph(int components)
        {
            dependencies = new Boolean[255, 255];
        }

        int getOrCreateIndex(string componentName)
        {
            int index;
            if (!componentIndex.ContainsKey(componentName))
            {
                index = lastDependencyIndex++;
                componentIndex.Add(componentName, index);
            }
            else
            {
                index = componentIndex[componentName];
            }
            return index;
        }

        public void insertDependency(Dependency dependency)
        {
            int dependentIndex = getOrCreateIndex(dependency.dependent);
            int dependencyIndex = getOrCreateIndex(dependency.dependency);

            if (dependencies[dependencyIndex, dependentIndex])
            {
                throw new CircularDependencyException(dependency.dependent + "," + dependency.dependency);
            }
            else
            {
                if (dependencies[dependentIndex, dependencyIndex] == false)
                {
                    DependencyCount++;
                    dependencies[dependentIndex, dependencyIndex] = true;
                }
            }
        }

        public void removeDependency(Dependency dependency)
        {
            int dependentIndex = getOrCreateIndex(dependency.dependent);
            int dependencyIndex = getOrCreateIndex(dependency.dependency);

            if (dependencies[dependentIndex, dependencyIndex] == true)
            {
                DependencyCount--;
                dependencies[dependentIndex, dependencyIndex] = false;
            }
        }

        Boolean dependencyTest(string dependent, string dependency)
        {
            return dependencyTest(getOrCreateIndex(dependent), getOrCreateIndex(dependency));
        }

        Boolean dependencyTest(int dependentIndex, int dependencyIndex)
        {
            return dependencies[dependentIndex, dependencyIndex];
        }

        public List<string> getDependencies(string componentName)
        {
            List<string> dependencies = new List<string>();

            if (!componentIndex.ContainsKey(componentName))
                return dependencies;

            int sourceComponentIdx = componentIndex[componentName];
            
            for (int i = 0; i < DependencyCount + 1; i++)
            {
                if (dependencyTest(sourceComponentIdx, i))
                {
                    var key = (from k in componentIndex where int.Equals(k.Value, i) select k.Key).FirstOrDefault();
                    dependencies.Add(key);
                }
            }

            return dependencies;
        }

    }
}
