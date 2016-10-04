/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Collections.Generic;
using Ndi.Component.Locator;
using Ndi.Exceptions;

namespace Ndi.Container
{
    class TreeContainer : DefaultContainer
    {
        List<IMutableContainer> childContainers = new List<IMutableContainer>();

		public TreeContainer(ILocator componentLocator) : base(componentLocator) { }

        public new object GetComponent(string componentName)
        {
            if (HasComponent(componentName))
            {
                return GetComponent(componentName);
            }
            else
            {
                foreach (IMutableContainer childContainer in childContainers)
                {
                    if (childContainer.HasComponent(componentName))
                    {
                        return childContainer.GetComponent(componentName);
                    }
                }
            }

            // we are sure that it was not in the childContainers   
            throw new InvalidComponentException(componentName);
        }

        public new void AddComponent(string componentName, object component)
        {
            if (!componentIsInChildContainers(componentName))
            {
                base.AddComponent(componentName, component);
            }
        }

        private Boolean componentIsInChildContainers(string componentName)
        {
            foreach (IMutableContainer childContainer in childContainers)
            {
                if (childContainer.HasComponent(componentName))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
