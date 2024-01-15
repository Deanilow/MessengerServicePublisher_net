using MessengerServicePublisher.Core.Common;
using MessengerServicePublisher.Core.Interfaces;
using MessengerServicePublisher.Core.Services;
using MessengerServicePublisher.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pilgaard.BackgroundJobs;
using Serilog;
using LoggerConfigurationExtensions =
    MessengerServicePublisher.Infrastructure.Logger.LoggerConfigurationExtensions;

var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", true, true)
           .Build();

var dotnetEnvironment = configuration["ENVIRONMENT"];

Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", dotnetEnvironment ?? "Production");

var host = Host.CreateDefaultBuilder(args)
     .ConfigureLogging((hostContext, services) =>
     {
         LoggerConfigurationExtensions.SetupLoggerConfiguration(hostContext);
     })
    .UseSerilog()
    .ConfigureHostConfiguration(configureHost =>
    {
        configureHost.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", false, true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddWindowsService(options =>
        {
            options.ServiceName = ".NET Joke Service";
        });
        services.AddSingleton<IEntryPointGmailService, EntryPointGmailService>();
        services.AddSingleton<IServiceLocator, ServiceScopeFactoryLocator>();
        services.AddInfrastructureServices(hostContext.Configuration);
        services.AddMemoryCache();
        //services.AddSingleton<TimerChangeWorker>();
        var Configuration = hostContext.Configuration;

        var applicationSettings = Configuration.GetSection("AppSettings").Get<Settings>();
        services.AddSingleton<ISettings, Settings>(e => applicationSettings);

        if (applicationSettings.PermissionGmail.ToUpper() == "TRUE")
        {
            services.AddBackgroundJobs().AddJob<WorkerPermissionGmail>();
        }
        else
        {
            if (applicationSettings.Cycle == "1")
            {
                services.AddBackgroundJobs().AddJob<WorkerOne>();
            }

            if (applicationSettings.Cycle == "x")
            {
                services.AddBackgroundJobs().AddJob<WorkerJob>();
            }
        }
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        // Ejecutar el TimerChangeWorker
        //var timerChangeWorker = serviceProvider.GetRequiredService<TimerChangeWorker>();
        //await timerChangeWorker.StartAsync(CancellationToken.None);

        // Ejecutar la aplicación
        await host.RunAsync();
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"{exception}");
    }
    finally
    {
        // Detener el TimerChangeWorker al finalizar
        //var timerChangeWorker = serviceProvider.GetRequiredService<TimerChangeWorker>();
        //await timerChangeWorker.StopAsync(CancellationToken.None);
    }
}