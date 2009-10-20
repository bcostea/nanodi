﻿#region Copyright 2009 Bogdan COSTEA
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
    public sealed class ApplicationContext
    {
    	private static IMutableContainer container = new DefaultContainer();
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

        ApplicationContext()
        {
            container.Initialize();
        }

        #endregion

        #region Component accessors

        public static object GetComponent(string componentName)
        {
            return container.GetComponent(componentName);
        }

        #endregion

        public static UtilityToolbox Toolbox { get { return toolbox; } }
    }
}
