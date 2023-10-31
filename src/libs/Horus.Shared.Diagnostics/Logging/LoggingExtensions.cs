using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Horus.Shared.Diagnostics.Logging;

public static class LoggingExtensions
{
    public static Logger GetBaseLogger(IHostEnvironment environment)
    {
        return new LoggerConfiguration()
            .UseDefaultConfiguration(environment)
            .CreateLogger();
    }

    public static IHostBuilder AddMegalodonLogging(this IHostBuilder builder, bool useDefaultConfiguration = true)
    {
        builder.ConfigureSerilogLogging(useDefaultConfiguration);

        return builder;
    }
    
    internal static void AddHorusLogging(this WebApplicationBuilder builder, bool useDefaultConfiguration = true)
    {
        builder.Host.ConfigureSerilogLogging(useDefaultConfiguration);
    }
    
    public static LoggerConfiguration UseDefaultConfiguration(this LoggerConfiguration configuration, IHostEnvironment environment)
    {
        const string template = "[<sc:{SourceContext}> ({Timestamp:HH:mm:ss}) {Level:u3}] {Message:lj} {NewLine}{Exception}";

        configuration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Grpc", LogEventLevel.Warning)
            .MinimumLevel.Override("OpenIddict", LogEventLevel.Warning)
            .MinimumLevel.Override("Serilog.AspNetCore.RequestLoggingMiddleware", LogEventLevel.Warning)
            .MinimumLevel.Override("ProtoBuf.Grpc", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .WriteTo.Seq(environment.IsDevelopment() ? "http://localhost:5341" : "http://seq:5341");
        
        var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isDevelopment = string.IsNullOrWhiteSpace(currentEnvironment) || currentEnvironment == "Development";
        
        if (isDevelopment)
        {
            configuration.WriteTo.Console(LogEventLevel.Information, template);
        }
        else
        {
            configuration.WriteTo.Console(new RenderedCompactJsonFormatter());
        }

        if (isDevelopment)
        {
            configuration.WriteTo.File("logs/.log", LogEventLevel.Information, template, 
                rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3);
        }
        
        return configuration;
    }
    
    private static void ConfigureSerilogLogging(this IHostBuilder hostBuilder, bool useDefaultConfiguration = true)
    {
        hostBuilder.UseSerilog(
            (context, services, configuration) =>
            {
                if (useDefaultConfiguration)
                {
                    configuration.UseDefaultConfiguration(context.HostingEnvironment);
                }
                else
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services);
                }
            });
    }
}