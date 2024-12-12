using LeTrack.BackgroundServices;
using LeTrack.Extensions;
using LeTrack.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);

builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService<EventSubscriber>();
builder.Services.AddHostedService<EventEmulator>();

builder.Services.ConfigureGeneral();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var serviceProvider = app.Services;
var mqttService = serviceProvider.GetRequiredService<MqttService>();
await mqttService.ConnectMqttAsync();

app.Run();
