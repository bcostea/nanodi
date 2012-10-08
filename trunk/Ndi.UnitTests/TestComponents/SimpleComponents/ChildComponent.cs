using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi.UnitTests.TestComponents.SimpleComponents
{
    class ChildComponent:IChildComponent
    {
        string content;
        public string Content
        {
            get { return content; }
            set { this.content = value; }
        }
    }
}
