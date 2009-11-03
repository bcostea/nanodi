using System;
using NanoDI.Attributes;


namespace NanoDIUnitTests.TestComponents.CircularDependencies
{
    [Component("dependencyTwo")]
    class DependencyTwo
    {
        [Inject]
        ICircularDependency dependencyOne;

        public ICircularDependency OtherDependency { get { return dependencyOne; } }
    }
}
