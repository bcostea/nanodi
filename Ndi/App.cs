/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

[assembly: CLSCompliant(true)]
namespace Ndi
{

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
