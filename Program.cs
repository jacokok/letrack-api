global using FastEndpoints;

using LeTrack.BackgroundServices;
using LeTrack.Extensions;
using LeTrack.Hubs;
using LeTrack.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);

builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService<EventSubscriber>();
builder.Services.AddHostedService<EventEmulator>();

builder.Services.ConfigureGeneral();
builder.Services.ConfigureEF(builder.Configuration, builder.Environment);
builder.Services.ConfigureApi();

var app = builder.Build();

app.UseApi();
app.MapHub<LeTrackHub>("/hub");

var serviceProvider = app.Services;
var mqttService = serviceProvider.GetRequiredService<MqttService>();
await mqttService.ConnectMqttAsync();

app.Run();
