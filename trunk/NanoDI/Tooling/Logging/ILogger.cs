using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoDI.Tooling.Logging
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
