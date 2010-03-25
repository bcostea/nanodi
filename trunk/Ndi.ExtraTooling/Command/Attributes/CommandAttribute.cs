/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using Ndi.Attributes;
using System;

namespace Ndi.Tooling.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandAttribute : ComponentAttribute
    {
        public CommandAttribute(string name, string key, string description, string helpText) 
            : base(name, Scope.Prototype) 
        {
            Key = key;
            Description = description;
            HelpText = helpText;
        }

        public string Key { get; private set; }
        public string Description { get; private set; }
        public string HelpText { get; private set; }
    }
}
