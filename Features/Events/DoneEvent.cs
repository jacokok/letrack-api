namespace LeTrack.Features.Events;

public class DoneEvent : IEvent
{
    public Guid Id { get; set; }
    public int TrackId { get; set; }
}