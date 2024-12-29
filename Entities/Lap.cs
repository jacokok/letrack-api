namespace LeTrack.Entities;

public class Lap
{
    public Guid Id { get; set; }
    public Guid? LastLapId { get; set; }
    public int TrackId { get; set; }
    public DateTime Timestamp { get; set; }
    public TimeSpan? LapTime { get; set; }
    public TimeSpan? LapTimeDifference { get; set; }
    public bool IsFlagged { get; set; }
    public string? FlagReason { get; set; }
    public int RaceId { get; set; }
}