using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Text;

namespace OffensivePowerShellTasking
{
    public class CustomPSHostUserInterface : PSHostUserInterface
    {
        // Replace StringBuilder with whatever your preferred output method is (e.g. a socket or a named pipe)
        public StringBuilder output { get; set; }
        private CustomPSRHostRawUserInterface _rawUi = new CustomPSRHostRawUserInterface();

        public CustomPSHostUserInterface()
        {
            output = new StringBuilder();
        }

        public CustomPSHostUserInterface(ref StringBuilder sb)
        {
            output = sb;
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            output.Append(value);
        }

        public override void WriteLine()
        {
            output.Append("\n");
        }

        public override void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            output.Append(value + "\n");
        }

        public override void Write(string value)
        {
            output.Append(value);
        }

        public override void WriteDebugLine(string message)
        {
            output.AppendLine("DEBUG: " + message);
        }

        public override void WriteErrorLine(string value)
        {
            output.AppendLine("ERROR: " + value);
        }

        public override void WriteLine(string value)
        {
            output.AppendLine(value);
        }

        public override void WriteVerboseLine(string message)
        {
            output.AppendLine("VERBOSE: " + message);
        }

        public override void WriteWarningLine(string message)
        {
            output.AppendLine("WARNING: " + message);
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            return;
        }

        public string Output
        {
            get { return output.ToString(); }
        }

        public override Dictionary<string, PSObject> Prompt(string caption, string message, System.Collections.ObjectModel.Collection<FieldDescription> descriptions)
        {
            throw new NotImplementedException("Prompt is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override int PromptForChoice(string caption, string message, System.Collections.ObjectModel.Collection<ChoiceDescription> choices, int defaultChoice)
        {
            throw new NotImplementedException("PromptForChoice is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
        {
            throw new NotImplementedException("PromptForCredential1 is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException("PromptForCredential2 is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override PSHostRawUserInterface RawUI
        {
            get { return _rawUi; }
        }

        public override string ReadLine()
        {
            throw new NotImplementedException("ReadLine is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override System.Security.SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException("ReadLineAsSecureString is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }
    }
}
