using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;

namespace OffensivePowerShellTasking
{
    public class PSTaskManager
    {
        private List<PSTask> TaskList = new List<PSTask>();

        public PSTaskManager() { }

        public int Execute(string ScriptBlock)
        {
            PSTask task = new PSTask();
            task.Initialize(ScriptBlock);
            task.Start();

            TaskList.Add(task);

            return task.ID;
        }

        public List<PSTask> GetAllTasks()
        {
            return TaskList;
        }

        public PSTask GetTask(int TaskId)
        {
            foreach (PSTask task in TaskList)
            {
                if (task.ID == TaskId)
                {
                    return task;
                }
            }

            return null;
        }

        public void KillAllTasks()
        {
            for (int i = TaskList.Count - 1; i >= 0; i--)
            {
                PSTask task = TaskList[i];
                task.Shutdown();
                TaskList.Remove(task);
            }
        }

        public void KillTask(int TaskId)
        {
            for (int i = TaskList.Count - 1; i >= 0; i--)
            {
                PSTask task = TaskList[i];
                if (task.ID == TaskId)
                {
                    task.Shutdown();
                    TaskList.Remove(task);
                }
            }
        }
    }
}
