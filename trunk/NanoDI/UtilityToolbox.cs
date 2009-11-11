using System;
using System.Reflection;
using System.Diagnostics;
using Ndi.Attributes;
using Ndi.Tooling.Logging;

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

		public static Scope GetScope(string scopeName)
		{
			Scope scope = Scope.Singleton;

			if (scopeName != null)
			{
				try
				{
					return (Scope)Enum.Parse(typeof(Scope), scopeName, true);
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
