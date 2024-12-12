namespace LeTrack.Features.Events;

public class SaveEventHandler : IEventHandler<SaveEvent>
{
    private readonly ILogger _logger;

    public SaveEventHandler(ILogger<SaveEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(SaveEvent eventModel, CancellationToken ct)
    {
        _logger.LogInformation($"order created event received:[{eventModel.Id}]");
        return Task.CompletedTask;
    }
}