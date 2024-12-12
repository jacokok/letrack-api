namespace LeTrack.Entities;

public class Event
{
    public Guid Id { get; set; }
    public int TrackId { get; set; }
    public DateTime Timestamp { get; set; }
}