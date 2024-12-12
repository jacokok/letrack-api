
using System.Text.Json;
using LeTrack.Models;
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
            await Task.Delay(5000, stoppingToken);

            EventModel eventModel = new() { Timestamp = DateTime.Now, TrackId = 1, Id = Guid.NewGuid() };
            string payload = JsonSerializer.Serialize(eventModel);
            _mqttService.Publish("event", payload);
        }
    }
}