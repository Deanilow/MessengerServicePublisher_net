﻿using MessengerServicePublisher.Core.Common;
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

Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

var host = Host.CreateDefaultBuilder(args)
     .ConfigureLogging((hostContext, services) =>
     {
         LoggerConfigurationExtensions.SetupLoggerConfiguration(hostContext);
     })
    .UseSerilog()
    .UseWindowsService()
    .ConfigureHostConfiguration(configureHost =>
    {
        configureHost.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", false, true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IEntryPointGmailService, EntryPointGmailService>();
        services.AddSingleton<IServiceLocator, ServiceScopeFactoryLocator>();
        services.AddInfrastructureServices(hostContext.Configuration);
        services.AddMemoryCache();

        var Configuration = hostContext.Configuration;

        var applicationSettings = Configuration.GetSection("AppSettings").Get<Settings>();
        services.AddSingleton<ISettings, Settings>(e => applicationSettings);

        services.AddBackgroundJobs()
          .AddJob<Worker>();
    })
    .Build();

try
{
    await host.RunAsync();
}
catch (Exception exception)
{
    var logger = host.Services.GetService<ILogger<Program>>();
    logger.LogInformation($"{exception}");
}