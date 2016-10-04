/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Ndi.Attributes;
using Ndi.Tooling.Logging;
using Ndi.Exceptions;

namespace Ndi
{
    public class UtilityToolbox
    {

        static ILogger log = LogFactory.GetLog("UtilityToolbox");
        int debugCheckHelper = -1;


        /// <summary>
        /// Checks if the assembly is currently started in debug mode.
        /// </summary>
        /// <returns></returns>
        public Boolean IsDebugEnabled()
        {
            if (debugCheckHelper < 0)
            {
                Assembly thisAssembly = Assembly.GetAssembly(typeof(AbstractApplicationContext));
                DebuggableAttribute debuggable = (DebuggableAttribute)DebuggableAttribute.GetCustomAttribute(thisAssembly, typeof(DebuggableAttribute));

                debugCheckHelper = debuggable != null && debuggable.IsJITTrackingEnabled ? 1 : 0;
            }
            return debugCheckHelper == 1;
        }

        /// <summary>
        /// Utility method that returns a type by type name
        /// </summary>
        /// <param name="typeName">The fully qualified name of the type that is required</param>
        /// <returns>Type</returns>
        public static Type GetType(string typeName)
        {
            Type type = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                type = asm.GetType(typeName);
                if (type != null)
                    break;
            }
            return type;
        }
        
        /// <summary>
        /// Utility method that returns a scope by scope name
        /// </summary>
        /// <param name="scopeName">The name of the scope, one of [Singleton, Prototype]</param>
        /// <returns>the scope, defaults to Scope.Singleton</returns>
        public static Scope GetScope(string scopeName)
        {
            Scope scope = Scope.Singleton;

            if (scopeName != null)
            {
                try
                {
                    return (Scope) Enum.Parse(typeof(Scope), scopeName, true);
                }
                catch (Exception e)
                {
                    if (log.IsDebugEnabled())
                    {
                        log.Debug(e.Message);
                    }
                }
            }
            return scope;

        }
    }
}
