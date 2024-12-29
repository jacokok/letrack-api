namespace LeTrack.Entities;

public class Race
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<RaceTrack> RaceTracks { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? RestartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? EndLapCount { get; set; }
    public TimeSpan? TimeRemaining { get; set; }
}