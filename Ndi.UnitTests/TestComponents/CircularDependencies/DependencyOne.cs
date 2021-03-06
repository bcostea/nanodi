﻿using System;
using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.CircularDependencies
{
    [Component("dependencyOne")]
    class DependencyOne:ICircularDependency
    {
        [Inject]
        ICircularDependency dependencyTwo=null;

        public ICircularDependency OtherDependency { get { return dependencyTwo; } }
    }
}
