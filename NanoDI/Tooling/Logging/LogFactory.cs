using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace NanoDI.Tooling.Logging
{
    public sealed class LogFactory
    {
        static Dictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();
        static UtilityToolbox toolbox = new UtilityToolbox();

        #region Singleton

        private static readonly LogFactory instance = new LogFactory();

        public static LogFactory Instance
        {
            get
            {
                return instance;
            }
        }

        LogFactory()
        {
        }

        #endregion

        public static Boolean IsDebugEnabled()
        {
            return toolbox.IsDebugEnabled();
        }


        public static ILogger GetLog(string name)
        {
            return getOrCreateLog(name);
        }

        /// <summary>
        /// This is a convenience method that uses the specified type's name as a logger name.
        /// </summary>
        /// <param name="type">The type that will be used for the logger name.</param>
        /// <returns>A logger instance</returns>
        public static ILogger GetLog(Type type)
        {
            return getOrCreateLog(type.Name);
        }

        static ILogger getOrCreateLog(string name)
        {
            if (!loggers.Keys.Contains(name))
            {
                ILogger logger = new ConsoleLogger(name);
                loggers.Add(name, logger);
            }

            return loggers[name];
        }

    }
}
