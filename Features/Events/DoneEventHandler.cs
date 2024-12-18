using LeTrack.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LeTrack.Features.Events;

public class DoneEventHandler : IEventHandler<DoneEvent>
{
    private readonly IHubContext<LeTrackHub, ILeTrackHub> _hub;

    public DoneEventHandler(IHubContext<LeTrackHub, ILeTrackHub> hub)
    {
        _hub = hub;
    }

    public async Task HandleAsync(DoneEvent eventModel, CancellationToken ct)
    {
        await _hub.Clients.All.DoneEvent(eventModel);
        return;
    }
}
