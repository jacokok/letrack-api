using LeTrack.Entities;

namespace LeTrack.Features.Track.Summary;

public class Response
{
    public List<Lap> Last10Laps { get; set; } = new();
    public Lap? FastestLap { get; set; }
    public int TotalLaps { get; set; }
    public List<Lap> Laps { get; set; } = new();
}