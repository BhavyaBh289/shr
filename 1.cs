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

ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonExePath,
            Arguments = pythonArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();

                // Read the output from the script
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Print the output and errors
                Console.WriteLine("Output:");
                Console.WriteLine(output);

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    Console.WriteLine("Errors:");
                    Console.WriteLine(errors);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while running the Python script.");
            Console.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
