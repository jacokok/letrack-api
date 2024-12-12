using LeTrack.Models;

namespace LeTrack.Hubs;

public interface ILeTrackHub
{
    Task ReceiveMessage(string message);
    Task ReceiveEvent(EventModel evt);
}