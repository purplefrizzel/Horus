using Horus.BBC.Core.Http;
using Horus.BBC.RpcService;
using Horus.Shared.Diagnostics;
using Horus.Shared.Diagnostics.Logging;
using ProtoBuf.Grpc.Server;
using Serilog.Core;

Logger? logger = null;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var environment = builder.Environment;
    logger = LoggingExtensions.GetBaseLogger(environment);
    
    builder.AddDiagnostics("Horus.BBC.RpcService", "1.0.0");
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.AddServerHeader = false;
        options.AllowResponseHeaderCompression = true;
        options.AllowAlternateSchemes = true;
    });

    builder.Services.AddHttpBBCWeatherService();
    builder.Services.AddCodeFirstGrpc();
    builder.Services.AddCodeFirstGrpcReflection();
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.MapGrpcService<BBCWeatherService>();
    app.MapCodeFirstGrpcReflectionService();
    
    logger.Information("The BBC RPC Service is starting on {MachineName} in {EnvironmentName} mode", 
        Environment.MachineName, app.Environment.EnvironmentName);
    logger.Information("The BBC RPC Service is listening on {Urls}", string.Join(", ", app.Urls));
    
    app.Run();
}
catch (Exception exception)
{
    logger?.Error(exception, "Failed to start service with exception");
}
finally
{
    logger?.Dispose();
}