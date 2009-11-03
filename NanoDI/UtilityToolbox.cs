using System;
using System.Reflection;
using System.Diagnostics;

namespace NanoDI
{
    public class UtilityToolbox
    {

        int debugCheckHelper = -1;


        /// <summary>
        /// Checks if the assembly is currently started in debug mode.
        /// </summary>
        /// <returns></returns>
        public Boolean IsDebugEnabled()
        {
            if (debugCheckHelper < 0)
            {
                Assembly thisAssembly = Assembly.GetAssembly(typeof(ApplicationContext));
                DebuggableAttribute debuggable = (DebuggableAttribute)DebuggableAttribute.GetCustomAttribute(thisAssembly, typeof(DebuggableAttribute));

                debugCheckHelper = debuggable != null && debuggable.IsJITTrackingEnabled ? 1 : 0;
            }
            return debugCheckHelper == 1;
        }

    }
}
