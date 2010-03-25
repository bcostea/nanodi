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
                foreach (IContainer childContainer in childContainers)
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

        Boolean componentIsInChildContainers(string componentName)
        {
            foreach (IContainer childContainer in childContainers)
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
