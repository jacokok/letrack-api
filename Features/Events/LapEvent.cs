namespace LeTrack.Features.Events;

public class LapEvent : IEvent
{
    public Guid Id { get; set; }
    public int TrackId { get; set; }
    public DateTime Timestamp { get; set; }
}