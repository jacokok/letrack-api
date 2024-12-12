using System.Text.Json;
using LeTrack.Models;
using LeTrack.Services;
using MQTTnet;
using MQTTnet.Client;

namespace LeTrack.BackgroundServices;

public class EventSubscriber : BackgroundService
{
    private readonly MqttService _mqttService;
    private readonly ILogger<EventSubscriber> _logger;

    public EventSubscriber(MqttService mqttService, ILogger<EventSubscriber> logger)
    {
        _mqttService = mqttService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // await _mqttService.ConnectMqttAsync();
        // while (!stoppingToken.IsCancellationRequested)
        // {
        _mqttService.GetMqttClient().ApplicationMessageReceivedAsync += ReceiveMessage;

        var mqttSubscribeOptions = _mqttService.GetMqttFactory().CreateSubscribeOptionsBuilder().WithTopicFilter("event/#").Build();

        var result = await _mqttService.GetMqttClient().SubscribeAsync(mqttSubscribeOptions, stoppingToken);
        _logger.LogInformation("Done");
    }

    private Task ReceiveMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        var eventModel = JsonSerializer.Deserialize<EventModel>(e.ApplicationMessage.ConvertPayloadToString());
        _logger.LogInformation("Id: {id}, Track: {track}, Timestamp: {ts}", eventModel?.Id, eventModel?.TrackId, eventModel?.Timestamp);

        // This should check if event exists. If not save to event table.
        // This should then trigger multiple fire and forget jobs.
        // SignalR send notify event to client
        // Get lap counts
        // Check what session this is part of
        // Send Summary to client
        return Task.CompletedTask;
    }
}