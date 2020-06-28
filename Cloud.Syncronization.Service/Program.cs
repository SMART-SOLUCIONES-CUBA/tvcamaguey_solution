using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Service.Extensions;
using System;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Service
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var isService = !(System.Diagnostics.Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                var pathToExe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = System.IO.Path.GetDirectoryName(pathToExe);

                System.IO.Directory.SetCurrentDirectory(pathToContentRoot);
            }

            var hostBuilder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    config.AddCommandLine(args);
                })
                .ConfigureLogging((hostContext, config) =>
                {
                    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(hostContext.Configuration).CreateLogger();

                    config.AddSerilog(Log.Logger);

                    if (!isService)
                    {
                        config.AddConsole();
                        config.AddDebug();
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<Options.SyncronizationHandlingOptions>(hostContext.Configuration.GetSection("SyncronizationHandling"));

                    services.AddLogging();

                    services.AddHostedService<SyncronizationHostedService>();
                });

            if (isService)
            {
                hostBuilder.UseServiceBaseLifetime();
            }
            else
            {
                hostBuilder.UseConsoleLifetime();
            }

            using (var host = hostBuilder.Build())
            {
                await host.StartAsync();

                await host.WaitForShutdownAsync();
            }
        }
    }
}
