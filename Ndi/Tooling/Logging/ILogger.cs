﻿/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

namespace Ndi.Tooling.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);

        Boolean IsDebugEnabled();
        string Name{get;}

    }
}
