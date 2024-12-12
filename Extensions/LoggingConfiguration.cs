using Serilog;

namespace LeTrack.Extensions;

public static class LoggingConfiguration
{
    public static void AddLoggingConfiguration(this IHostBuilder host, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        host.UseSerilog();
    }
}