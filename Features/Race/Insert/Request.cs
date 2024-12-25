namespace LeTrack.Features.Race.Insert;

public class Request
{
    public string Name { get; set; } = string.Empty;
    // public List<RaceTrack> RaceTracks { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? EndLapCount { get; set; }
}