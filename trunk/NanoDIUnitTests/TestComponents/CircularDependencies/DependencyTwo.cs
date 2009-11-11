using System;
using Ndi.Attributes;


namespace NdiUnitTests.TestComponents.CircularDependencies
{
    [Component("dependencyTwo")]
    class DependencyTwo
    {
        [Inject]
        ICircularDependency dependencyOne=null;

        public ICircularDependency OtherDependency { get { return dependencyOne; } }
    }
}
