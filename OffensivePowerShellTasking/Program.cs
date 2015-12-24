using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Management.Automation.Host;

namespace OffensivePowerShellTasking
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main function's AppDomain: " + AppDomain.CurrentDomain.FriendlyName);

            PSTaskManager TaskManager = new PSTaskManager();

            while (true)
            {
                StringBuilder output = new StringBuilder();

                // 1. Check for any pending commands (new job, kill job(s), list job(s), checkin on job)
                string input = RecieveInput();

                // 2. Execute incoming command (new job, status, stop, exit, etc.)

                List<PSTask> tasks = TaskManager.GetAllTasks();
                if (input == "status")
                {
                    foreach (PSTask task in tasks)
                    {
                        Console.WriteLine(task.ID + "\t" + task.GetStatus());
                    }
                }
                else if(input == "exit")
                {
                    try
                    {
                        TaskManager.KillAllTasks();
                    }
                    catch (Exception e)
                    {
                        // Do something with the error...
                        SendOutput(e.ToString());
                    }

                    // TODO: Return the output of any tasks here...

                    break;
                }
                else if (input != String.Empty)
                {
                    int TaskID = TaskManager.Execute(input);

                    // 2b.  Return the TaskID
                }

                // 3. Remove stopped/completed jobs and store output
                // Count backwards so we don't skip anything in case we have to remove something
                for (int i = tasks.Count - 1; i >= 0; i--)
                {
                    PSTask t = tasks[i];
                    PipelineState status = t.GetStatus();
                    if (status == PipelineState.Completed || status == PipelineState.Failed || status == PipelineState.Stopped)
                    {
                        // Step 3b. - Create the return data structure
                        output.Append("\n\n\n************ Task " + t.ID + " Output ************\n");
                        output.Append("Status: " + t.GetStatus() + "\n");
                        output.Append("Output: " + t.GetOutput());

                        // Step 3c. - Destroy/remove the job
                        try
                        {
                            TaskManager.KillTask(t.ID);
                        }
                        catch (Exception e)
                        {
                            // Do something with the error...
                            SendOutput(e.ToString());
                        }

                    }
                }

                // Step 3b - Manually invoke the garbage collector to free decrease memory usage...
                InvokeGarbageCollector();

                // 4. Return output (output from steps 2 and 3)
                SendOutput(output.ToString());
            }
        }

        private static string RecieveInput()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }

        private static void SendOutput(string output)
        {
            Console.WriteLine(output);
        }

        public static void InvokeGarbageCollector()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
