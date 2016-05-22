using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Actions
{
    public class Process
    {
        public void StartProcess(string name)
        {
            this.StartProcess(name, null);
        }

        public void StartProcess(string name, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = name,
                Arguments = arguments
            };

            System.Diagnostics.Process.Start(psi);
        }
    }
}
