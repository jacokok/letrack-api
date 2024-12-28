using LeTrack.DTO;
using LeTrack.Entities;

namespace LeTrack.Features.Race.Summary;

public class Response
{
    public List<Track> Tracks { get; set; } = new();
    public LapDTO? FastestLap { get; set; }
    public int TotalLaps { get; set; }
    public Entities.Race Race { get; set; } = default!;
}

public class Track
{
    public int TrackId { get; set; }
    public List<LapDTO> Laps { get; set; } = new();
    public Player Player { get; set; } = default!;
    public LapDTO? FastestLap { get; set; }
    public int TotalLaps { get; set; }
}