﻿/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

[assembly: CLSCompliant(true)]
namespace Ndi
{
    /// <summary>
    /// Utility class, singleton that contains the initialised application context
    /// </summary>
	public class App
	{

		#region Singleton
		private static readonly App instance = new App();

		public static App Instance
		{
			get
			{
				return instance;
			}
		}

		App()
		{
		}
		#endregion

		static AbstractApplicationContext applicationContext;

		public AbstractApplicationContext Context
		{
			get { return applicationContext; }
			set { applicationContext = value; }
		}
		
	}
}
