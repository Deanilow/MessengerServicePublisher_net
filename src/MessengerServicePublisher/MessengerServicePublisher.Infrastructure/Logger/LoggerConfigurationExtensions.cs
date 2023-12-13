using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace MessengerServicePublisher.Infrastructure.Logger
{
    public static class LoggerConfigurationExtensions
    {
        public static void SetupLoggerConfiguration(HostBuilderContext host)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(host.Configuration)
                .ConfigureBaseLogging()
                .CreateLogger();
        }

        public static LoggerConfiguration ConfigureBaseLogging(this LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithThreadId();

            return loggerConfiguration;
        }
    }
}
