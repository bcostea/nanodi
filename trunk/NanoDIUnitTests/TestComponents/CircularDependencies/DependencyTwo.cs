using System;
using NanoDI.Attributes;


namespace NanoDIUnitTests.TestComponents.CircularDependencies
{
    [Component("dependencyTwo")]
    class DependencyTwo
    {
        [Inject]
        ICircularDependency dependencyOne=null;

        public ICircularDependency OtherDependency { get { return dependencyOne; } }
    }
}
