/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Ndi.Exceptions;

namespace Ndi.Component.Dependency
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
