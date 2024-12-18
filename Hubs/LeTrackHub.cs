using Microsoft.AspNetCore.SignalR;

namespace LeTrack.Hubs;
public class LeTrackHub : Hub<ILeTrackHub>
{
    public async Task SendMessage(string message) => await Clients.All.ReceiveMessage(message);
}