using MQTTnet;
using MQTTnet.Client;

namespace LeTrack.Services;

public class MqttService
{
    private static readonly MqttFactory Factory = new();
    private readonly IMqttClient _mqttClient = Factory.CreateMqttClient();
    private const string BrokerServer = "localhost";

    public async Task ConnectMqttAsync()
    {
        if (_mqttClient.IsConnected) return;

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(BrokerServer)
            .Build();

        await _mqttClient.ConnectAsync(options, CancellationToken.None);
    }
    public MqttFactory GetMqttFactory()
    {
        return Factory;
    }
    public IMqttClient GetMqttClient()
    {
        return _mqttClient;
    }
    public void Publish(string topic, string payload)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithRetainFlag()
            .Build();

        _mqttClient.PublishAsync(message);
    }
}