using System;
using NanoDI.Attributes;

namespace NanoDIUnitTests.TestComponents.CircularDependencies
{
    [Component("dependencyOne")]
    class DependencyOne:ICircularDependency
    {
        [Inject]
        ICircularDependency dependencyTwo;

        public ICircularDependency OtherDependency { get { return dependencyTwo; } }
    }
}
