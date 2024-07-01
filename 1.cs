using System;
using Microsoft.Win32.TaskScheduler;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
//

      // Get the service on the local machine
using (TaskService ts = new TaskService("192.168.122.177","Administrator","Administrator" ,"asdf6^78")){
    // Create a new task definition and assign properties

    TaskDefinition ts = ts.NewTask();
    ts.RegistrationInfo.Description = "exec";
    ts.Triggers.Add(new TimeTrigger { StartBoundary = DateTime.Now + TimeSpan.FromMinutes(1) });

    ts.Actions.Add(new ExecAction(executablePath, arguments, null));
    ts.RootFolder.RegisterTaskDefinition(exec, td);
    Task t = ts.GetTask(exec);
    if (t != null){
        t.Run();
    }
    // Remove the task we just created
    ts.RootFolder.DeleteTask("Tdst");

}

