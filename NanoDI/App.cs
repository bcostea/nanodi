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
