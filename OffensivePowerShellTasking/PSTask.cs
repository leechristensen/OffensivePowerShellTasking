using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;

namespace OffensivePowerShellTasking
{
    public class PSTask
    {
        public int ID { get; set; }
        private AppDomain PSAppDomain = null;
        private PSExecutionItem task = null;
        private static int JobCounter = 0;

        public PSTask()
        {
            this.ID = JobCounter++;
        }

        public void Initialize(string PSScriptBlock)
        {
            PSAppDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), AppDomain.CurrentDomain.Evidence);

            // Create a new PSTask object in a separate app domain.  
            // Because PSExecutionitem implements MarshalByRefObject, CreateInstanceAndUnwrap returns a "proxy" object that we can use to query info about the remote object
            task = (PSExecutionItem)PSAppDomain.CreateInstanceAndUnwrap(
                Assembly.GetExecutingAssembly().FullName,
                typeof(PSExecutionItem).FullName,
                false,
                BindingFlags.Default,
                default(Binder),
                new object[] { PSScriptBlock },      // PSExecutionitem constructor arguments
                default(CultureInfo),
                null,
                AppDomain.CurrentDomain.Evidence
            );
        }

        public void Start()
        {
            if (task == null)
            {
                throw new Exception("Task has not been initialized");
            }

            task.Invoke();
        }

        public PipelineState GetStatus()
        {
            if (task == null)
            {
                throw new Exception("Task has not been initialized");
            }
            return task.GetState();
        }

        public StringBuilder GetOutput()
        {
            if (task == null)
            {
                throw new Exception("Task has not been initialized");
            }

            return task.GetOutput();
        }

        // Stops any current tasks and unloads the PSAppDomain.  Throws an exception if the domain can't be unloaded
        public void Shutdown()
        {
            if (task != null)
            {
                task.Stop();
                task = null;
            }

            if (PSAppDomain != null)
            {
                AppDomain.Unload(PSAppDomain);
                PSAppDomain = null;
            }
        }
    }
}
