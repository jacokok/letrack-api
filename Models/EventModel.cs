namespace LeTrack.Models;

public class EventModel
{
    public Guid Id { get; set; }
    public int TrackId { get; set; }
    public DateTime Timestamp { get; set; }
}