namespace LeTrack.Entities;

public class RaceTrack
{
    public int RaceId { get; set; }
    public int TrackId { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; } = default!;
}