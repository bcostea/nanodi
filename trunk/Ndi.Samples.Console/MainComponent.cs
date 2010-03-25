/** 
 * This File is part of the NDI Library Samples
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

using Ndi.Attributes;
using NdiExample.Dependencies;

namespace NdiExample
{
    /**
     * We declare this as a component using the NanoDI.Attributes.Component attribute
     * this name will be used for component autowire-ing
     */

    [Component("mainComponent")]
    class MainComponent:IMainComponent
    {
        // this is how we inject another Component.
        [Inject]
        IDependency dependencyOne = null;
        
        [Inject]
        IDependency dependencyTwo = null;
        
        [Inject]
        IDependency dependencyThree = null;

        public MainComponent()
        {

        }

        public void Start()
        {
            Console.WriteLine("Starting MainComponent");

            dependencyOne.Start();
            dependencyTwo.Start();
            dependencyThree.Start();
        }
    }
}
