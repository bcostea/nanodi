/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using Ndi.Container;

namespace Ndi
{
	public abstract class AbstractApplicationContext : IApplicationContext
    {

        public static readonly string DEFAULT_CONTEXT_FILE_NAME = @"components.xml";

		internal IMutableContainer container;
		Lifecycle contextLifecycle = new Lifecycle();

		protected AbstractApplicationContext()
        {
        }

		protected AbstractApplicationContext(string initializationParameter)
        {
			Initialize(initializationParameter);
        }

        public abstract void Initialize();
		public abstract void Initialize(string initializationParameter);
		public abstract void InitializeContainer(string initializationParameter);

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
            // add to application
            if (App.Instance.Context != null)
            {
                // merge treecontainers
                //    App.Instance.Context.
            }
            else
            {
                App.Instance.Context = this;
            }

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
