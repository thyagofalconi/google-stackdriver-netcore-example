using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoogleStackdriverNetCoreExample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.ServiceName = Configuration["Stackdriver:ServiceName"];
                options.Version = Configuration["Stackdriver:Version"];
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseGoogleExceptionLogging();
            
            loggerFactory.AddGoogle(Configuration["Stackdriver:ProjectId"]);

            var logger = loggerFactory.CreateLogger("TestApp");

            logger.LogInformation("This is a log message.");
        }
    }
}