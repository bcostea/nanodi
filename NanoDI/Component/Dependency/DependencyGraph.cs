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
        int lastDependencyIndex=0;
        int dependencyCount = 0;

        public DependencyGraph(int components)
        {
            dependencies = new Boolean[components][];
        }

        public void insertDependency(String dependent, String dependency)
        {
            int dependentIndex = getOrCreateIndex(dependent);
            int dependencyIndex = getOrCreateIndex(dependency);

            initDefaultDependency(dependentIndex);
            initDefaultDependency(dependencyIndex);

            validateNonCircularDependency(dependent, dependency);
            
            markAsDependent(dependentIndex, dependencyIndex);
        }

        void initDefaultDependency(int index){
            if (dependencies[index] == null)
                dependencies[index] = new Boolean[dependencies.Length];
        }

        void markAsDependent(int dependentIndex, int dependencyIndex){
            if (dependencies[dependentIndex][dependencyIndex] == false)
            {
                dependencies[dependentIndex][dependencyIndex] = true;
                dependencyCount++;
            }
        }

        void validateNonCircularDependency(string dependent, string dependency)
        {
            int dependentIndex = getOrCreateIndex(dependent);
            int dependencyIndex = getOrCreateIndex(dependency);

            if (dependencies[dependencyIndex][dependentIndex])
            {
                throw new CircularDependencyException(dependent + "," + dependency);
            }
        }

        Boolean dependencyTest(int dependentIndex, int dependencyIndex)
        {
            return dependencies[dependentIndex][dependencyIndex];
        }

        public List<string> getDependencies(string componentName)
        {
            List<string> dependencies = new List<string>();

            if (!components.ContainsKey(componentName))
                return dependencies;

            int sourceComponentIdx = components[componentName];
            
            for (int i = 0; i < dependencyCount + 1; i++)
            {
                if (dependencyTest(sourceComponentIdx, i))
                {
                    var key = (from k in components where int.Equals(k.Value, i) select k.Key).FirstOrDefault();
                    dependencies.Add(key);
                }
            }

            return dependencies;
        }

        int getOrCreateIndex(string componentName)
        {
            int index;
            if (!components.ContainsKey(componentName))
            {
                index = lastDependencyIndex++;
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
