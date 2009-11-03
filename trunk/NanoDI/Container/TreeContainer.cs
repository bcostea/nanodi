using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoDI.Exceptions;

namespace NanoDI.Container
{
    class TreeContainer : DefaultContainer
    {
        List<IMutableContainer> childContainers = new List<IMutableContainer>();

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
