
using System.Text.Json;
using LeTrack.Features.Events;
using LeTrack.Services;

namespace LeTrack.BackgroundServices;

public class EventEmulator : BackgroundService
{
    private readonly MqttService _mqttService;

    public EventEmulator(MqttService mqttService)
    {
        _mqttService = mqttService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int delayTime = new Random().Next(5000, 20000);
            await Task.Delay(delayTime, stoppingToken);

            int randomTrackId = new Random().Next(1, 3);

            SaveEvent eventModel = new() { Timestamp = DateTime.Now.ToUniversalTime(), TrackId = randomTrackId, Id = Guid.NewGuid() };
            string payload = JsonSerializer.Serialize(eventModel);
            _mqttService.Publish("event", payload);
        }
    }
}