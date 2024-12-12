using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LeTrack.Extensions;

public static class CorsExtensions
{
    private static readonly string _allowedOrigins = "LeTrackOrigins";

    public static void ConfigureCors(this IServiceCollection services, string[] origins)
    {
        services.AddCors(delegate (CorsOptions options)
        {
            options.AddPolicy(_allowedOrigins, delegate (CorsPolicyBuilder builder)
            {
                builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public static IApplicationBuilder UseCorsLeTrack(this IApplicationBuilder app)
    {
        app.UseCors(_allowedOrigins);
        return app;
    }
}