using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation.Host;
using System.Text;
using System.Threading;

namespace OffensivePowerShellTasking
{
    class CustomPSHost : PSHost
    {
        private Guid _hostId = Guid.NewGuid();
        public CustomPSHostUserInterface CustomUI { get; set; }

        public override Guid InstanceId
        {
            get { return _hostId; }
        }

        public override string Name
        {
            get { return "ConsoleHost"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override PSHostUserInterface UI
        {
            get { return CustomUI; }
        }

        public override CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public override CultureInfo CurrentUICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }

        public override void EnterNestedPrompt()
        {
            throw new NotImplementedException("EnterNestedPrompt is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override void ExitNestedPrompt()
        {
            throw new NotImplementedException("ExitNestedPrompt is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override void NotifyBeginApplication()
        {
            return;
        }

        public override void NotifyEndApplication()
        {
            return;
        }

        public override void SetShouldExit(int exitCode)
        {
            return;
        }
    }
}
