using System;
using System.IO;
using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace GoogleStackdriverNetCoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string credentialPath = @"C:\google.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => 
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((hostingContext, logging) => logging.AddProvider(GoogleLoggerProvider.Create("INSERT-PROJECT-ID-HERE")))
                .UseStartup<Startup>()
                .Build();
    }
}
