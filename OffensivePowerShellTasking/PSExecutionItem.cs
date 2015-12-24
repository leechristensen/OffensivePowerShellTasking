using System;
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Text;

namespace OffensivePowerShellTasking
{
    public class PSExecutionItem : MarshalByRefObject
    {
        private string ScriptBlock;
        private StringBuilder output = new StringBuilder();
        private Runspace runspace;                          // Can't expose these publicly since they aren't serializable
        private Pipeline pipeline;

        public PSExecutionItem(string ScriptBlock)
        {
            this.ScriptBlock = ScriptBlock;

            CustomPSHost host = new CustomPSHost();
            host.CustomUI = new CustomPSHostUserInterface(ref output);

            var state = InitialSessionState.CreateDefault();
            state.AuthorizationManager = null;                  // Set PowerShell's execution policy to bypass

            runspace = RunspaceFactory.CreateRunspace(host, state);
            runspace.Open();

            pipeline = runspace.CreatePipeline();
        }

        ~PSExecutionItem()
        {
            pipeline.Dispose();
            runspace.Dispose();
        }

        public PipelineState GetState()
        {
            return pipeline.PipelineStateInfo.State;
        }

        public void Stop()
        {
            // Aync?
            pipeline.Stop();
        }

        public StringBuilder GetOutput()
        {
            return output;
        }

        public void Invoke()
        {
            pipeline.Commands.AddScript(ScriptBlock);
            pipeline.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
            pipeline.Commands.Add("out-default");
            pipeline.InvokeAsync();
        }
    }
}
