/** 
 * This File is part of the NDI Library Samples
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using Ndi;

namespace NdiExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AbstractApplicationContext appCtx = new AttributeApplicationContext("NdiExample");

            IMainComponent mainComponent = (IMainComponent) appCtx.GetComponent("mainComponent");

            mainComponent.Start();

            Console.ReadLine();
        }
    }
}