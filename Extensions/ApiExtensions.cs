using FastEndpoints.Swagger;

namespace LeTrack.Extensions;

public static class ApiExtensions
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        services.AddFastEndpoints()
        .SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.Title = AppDomain.CurrentDomain.FriendlyName;
                s.Version = "v1";
            };
        });
    }

    public static IApplicationBuilder UseApi(this IApplicationBuilder app)
    {
        app.UseDefaultExceptionHandler()
        .UseFastEndpoints(c =>
        {
            c.Serializer.Options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            c.Endpoints.Configurator = ep =>
            {
                ep.Options(x => x.Produces<InternalErrorResponse>(500));
            };
        });
        app.UseSwaggerGen(_ => { }, ui => ui.Path = string.Empty);
        return app;
    }
}