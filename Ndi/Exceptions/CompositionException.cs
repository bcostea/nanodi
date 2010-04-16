/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

namespace Ndi.Exceptions
{
    /**
     *   This exception is thrown when: 
     *   - a problem initializing the container
     *   - a cyclic dependency between components occurs.
     *   - problem adding a component
     */

    [Serializable]
    public class CompositionException : Exception
    {
        public CompositionException()
        {

        }
        public CompositionException(string message)
            : base(message)
        {
        }
        public CompositionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
