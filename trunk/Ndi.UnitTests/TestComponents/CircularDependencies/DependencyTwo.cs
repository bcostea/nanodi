using System;
using Ndi.Attributes;


namespace Ndi.UnitTests.TestComponents.CircularDependencies
{
    [Component("dependencyTwo")]
    class DependencyTwo
    {
        [Inject]
        ICircularDependency dependencyOne=null;

        public ICircularDependency OtherDependency { get { return dependencyOne; } }
    }
}
