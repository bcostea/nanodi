/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Globalization;

namespace Ndi.Tooling.Logging
{
    public class ConsoleLogger : ILogger
    {
        String name;

        const string INFO_STRING = "[INFO] {0} : {1}";
        const string DEBUG_STRING = "[DEBUG] {0} : {1}";
        const string ERROR_STRING = "[ERROR] {0} : {1}";

        public string Name { get { return name; } }

        public ConsoleLogger(string name)
        {
            this.name = name;
        }

        public void Info(string message)
        {
            WriteMessage(INFO_STRING, message);
        }

        public void Debug(string message)
        {
            WriteMessage(DEBUG_STRING, message);
        }

        public void Error(string message)
        {
            WriteMessage(ERROR_STRING, message);
        }

        void WriteMessage(string template, string message)
        {
            System.Console.WriteLine(string.Format(CultureInfo.CurrentCulture, template, new object[] { name, message }));
        }

        ///<summary>
        /// Shortcut to LogFactory.IsDebugEnabled();
        ///</summary>
        public bool IsDebugEnabled()
        {
            return LogFactory.IsDebugEnabled();
        }

    }
}
