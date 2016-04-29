using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Server
{
    public class MissingWebServerRootPathException : Exception
    {
        public MissingWebServerRootPathException(string message) : base(message)
        {
        }
    }
}
