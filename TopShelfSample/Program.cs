using System.Runtime.InteropServices;
using Topshelf;
using Topshelf.Runtime.DotNetCore;
using TopShelfSample;

Console.WriteLine("MyService Starting");


// Main entry
var rc = HostFactory.Run(x =>
{
    // this works if running on Linux
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        x.UseEnvironmentBuilder(new Topshelf.HostConfigurators.EnvironmentBuilderFactory(c => {
            return new DotNetCoreEnvironmentBuilder(c);
        }));
    }

    x.Service<MyServiceHandler>(s =>
    {
        // The system can use dependency injection, if it's setup
        //s.ConstructUsing(name => ServiceProvider.GetService<MyService>());
        s.ConstructUsing(name => new MyServiceHandler());
        // The start function
        s.WhenStarted(tc => tc.Start());
        // The stop function
        s.WhenStopped(tc => tc.Stop());
    });

    // Run as local system user
    x.RunAsLocalSystem();
    // Description that shows up in the services control manager
    x.SetDescription("MyService TopShelf sample");
    // Displayed in services control manager
    x.SetDisplayName("My-Service");
    // Registry name
    x.SetServiceName("MyService");
    // Configure for delayed auto-start 
    x.StartAutomaticallyDelayed();
});

Console.WriteLine("MyService Halted");
var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
Environment.ExitCode = exitCode;

