#region Copyright 2009 Bogdan COSTEA
/** This File is part of the NanoDI Library
 *
 * Copyright 2009 Bogdan COSTEA
 * All rights reserved
 * 
 * This library is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published
 * by the Free Software Foundation; either version 2.1 of the License, or
 * (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this library; if not, write to the
 * Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor Boston, MA  02110-1301 USA
 */
#endregion

using System;
using System.Reflection;
using NanoDI;
using NanoDI.Component.ComponentActivator;
using NanoDI.Component.Locator;
using NanoDI.Component.Registry;
using NanoDI.Container;
using NanoDI.Exceptions;
using System.Diagnostics;

[assembly: CLSCompliant(true)]
namespace NanoDI
{
    public sealed class ApplicationContext : ILifecycle
    {
        private static IMutableContainer container;
        private static Lifecycle lifecycle = new Lifecycle();
        private static UtilityToolbox toolbox = new UtilityToolbox();
        
        #region Singleton

        private static readonly ApplicationContext instance = new ApplicationContext();

        public static ApplicationContext Instance
        {
            get
            {
                return instance;
            }
        }

        ApplicationContext(){}

        #endregion

        void ILifecycle.Initialize()
        {
            ApplicationContext.Initialize();
        }

        void ILifecycle.Destroy()
        {
            ApplicationContext.Destroy();
        }

        public static void Initialize()
        {
            beforeInitialize();
            initializeContainer(null);
            afterInitialize();
        }


        public static void Destroy()
        {
            lifecycle.BeforeDestroy();
            container.Destroy();
            lifecycle.Destroyed();
        }

        public static void Initialize(string targetNamespace)
        {
            beforeInitialize();
            initializeContainer(targetNamespace);
            afterInitialize();
        }

        private static void initializeContainer(string targetNamespace)
        {
            container = new DefaultContainer();
            
            if (targetNamespace == null)
            {
                container.Initialize();
            }
            else
            {
                container.Initialize(targetNamespace);
            }
        }

        private static void beforeInitialize()
        {
            lifecycle.NotInitializedRequired();
            lifecycle.BeforeInitialize();
        }


        private static void afterInitialize()
        {
            lifecycle.Initialized();
        }

        public static object GetComponent(string componentName)
        {
            lifecycle.InitializedRequired();
            return container.GetComponent(componentName);
        }

        public static UtilityToolbox Toolbox { get { return toolbox; } }
        public static Lifecycle Lifecycle { get { return lifecycle; } }
    }
}
