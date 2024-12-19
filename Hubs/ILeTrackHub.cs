using LeTrack.Features.Events;

namespace LeTrack.Hubs;

public interface ILeTrackHub
{
    Task ReceiveMessage(string message);
    Task ReceiveEvent(SaveEvent evt);
    Task DoneEvent(DoneEvent evt);
    Task RaceComplete(int raceId);
}