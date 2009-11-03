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

namespace NanoDI
{
	public abstract class AbstractApplicationContext : IApplicationContext
    {
	
		internal IMutableContainer container;
		Lifecycle contextLifecycle = new Lifecycle();

		protected AbstractApplicationContext()
        {
			Initialize();
        }

		protected AbstractApplicationContext(string initializationParameter)
        {
			Initialize(initializationParameter);
        }

		public abstract void Initialize(string initializationParameter);
		public abstract void InitializeContainer(string initializationParameter);

        public void Initialize()
        {
            beforeInitialize();
            InitializeContainer(null);
            afterInitialize();
        }

        public void Destroy()
        {
			contextLifecycle.BeforeDestroy();
            container.Destroy();
			contextLifecycle.Destroyed();
        }


        protected void beforeInitialize()
        {
			contextLifecycle.NotInitializedRequired();
			contextLifecycle.BeforeInitialize();
        }


		protected void afterInitialize()
        {
			contextLifecycle.Initialized();
        }

        public object GetComponent(string componentName)
        {
			contextLifecycle.InitializedRequired();
            return container.GetComponent(componentName);
        }

		public Lifecycle Lifecycle { get { return contextLifecycle; } }
    }
}
