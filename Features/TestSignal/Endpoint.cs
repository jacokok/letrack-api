using LeTrack.Data;
using LeTrack.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.TestSignal;

public class Endpoint : EndpointWithoutRequest<bool>
{
    private readonly IHubContext<LeTrackHub, ILeTrackHub> _hub;

    public Endpoint(IHubContext<LeTrackHub, ILeTrackHub> hub)
    {
        _hub = hub;
    }

    public override void Configure()
    {
        Get("/test/signal");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        await _hub.Clients.All.ReceiveMessage(DateTime.Now.ToString());
        await SendAsync(true);
    }
}