namespace LeTrack.Features.Events;

public class SaveEvent : IEvent
{
    public Guid Id { get; set; }
    public int TrackId { get; set; }
    public DateTime Timestamp { get; set; }
}