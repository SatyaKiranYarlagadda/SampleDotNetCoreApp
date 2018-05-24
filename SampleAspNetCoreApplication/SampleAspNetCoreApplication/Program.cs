using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SampleAspNetCoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // BuildWebHost(args).RunWhenHealthy(new System.TimeSpan(0,0,10));
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseHealthChecks("/health")
                .Build();
    }
}
