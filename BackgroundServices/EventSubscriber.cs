using System.Text.Json;
using LeTrack.Data;
using LeTrack.Entities;
using LeTrack.Features.Events;
using LeTrack.Services;
using MQTTnet;
using MQTTnet.Client;

namespace LeTrack.BackgroundServices;

public class EventSubscriber : BackgroundService
{
    private readonly MqttService _mqttService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EventSubscriber> _logger;

    public EventSubscriber(MqttService mqttService, ILogger<EventSubscriber> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _mqttService = mqttService;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _mqttService.GetMqttClient().ApplicationMessageReceivedAsync += ReceiveMessage;

        var mqttSubscribeOptions = _mqttService.GetMqttFactory().CreateSubscribeOptionsBuilder().WithTopicFilter("event/#").Build();

        var result = await _mqttService.GetMqttClient().SubscribeAsync(mqttSubscribeOptions, stoppingToken);
        _logger.LogInformation("Done");
    }

    private async Task ReceiveMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var eventModel = JsonSerializer.Deserialize<SaveEvent>(e.ApplicationMessage.ConvertPayloadToString());
        _logger.LogInformation("Id: {id}, Track: {track}, Timestamp: {ts}", eventModel?.Id, eventModel?.TrackId, eventModel?.Timestamp);

        if (eventModel == null)
        {
            _logger.LogError("Received eventModel that we could not parse and was null");
            return;
        }

        Event evt = new()
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId,
            Timestamp = eventModel.Timestamp
        };

        if (db.Event.Any(x => x.Id == evt.Id))
        {
            _logger.LogInformation("Event already exists");
            return;
        }

        await db.Event.AddAsync(evt);
        await db.SaveChangesAsync();

        await new SaveEvent
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId,
            Timestamp = eventModel.Timestamp
        }.PublishAsync(Mode.WaitForNone);



        // This should check if event exists. If not save to event table.
        // This should then trigger multiple fire and forget jobs.
        // SignalR send notify event to client
        // Get lap counts
        // Check what session this is part of
        // Send Summary to client
        return;
    }
}