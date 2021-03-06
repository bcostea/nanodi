﻿/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Runtime.Serialization;

namespace Ndi.Exceptions
{
    [Serializable]
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException(string components) :
            base("Circular dependency between components: " + components + " !")
        {

        }

        public CircularDependencyException()
        {

        }
        public CircularDependencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected CircularDependencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
