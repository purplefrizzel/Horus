using Horus.Shared.Diagnostics.Logging;
using Microsoft.AspNetCore.Builder;

namespace Horus.Shared.Diagnostics;

public static class DiagnosticsExtensions
{
    public static void AddDiagnostics(this WebApplicationBuilder builder, string? serviceName = default, 
        string? serviceVersion = default)
    {
        builder.AddHorusLogging();
    }
}