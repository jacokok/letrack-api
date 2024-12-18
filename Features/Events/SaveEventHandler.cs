using LeTrack.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LeTrack.Features.Events;

public class SaveEventHandler : IEventHandler<SaveEvent>
{
    private readonly IHubContext<LeTrackHub, ILeTrackHub> _hub;

    public SaveEventHandler(IHubContext<LeTrackHub, ILeTrackHub> hub)
    {
        _hub = hub;
    }

    public async Task HandleAsync(SaveEvent eventModel, CancellationToken ct)
    {
        await _hub.Clients.All.ReceiveEvent(eventModel);

        await new LapEvent
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId,
            Timestamp = eventModel.Timestamp
        }.PublishAsync();

        await new DoneEvent
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId
        }.PublishAsync();

        return;
    }
}