using LeTrack.DTO;

namespace LeTrack.Features.Track.Summary;

public class Response
{
    public List<LapDTO> Last10Laps { get; set; } = new();
    public LapDTO? FastestLap { get; set; }
    public int TotalLaps { get; set; }
    public Entities.Race? Race { get; set; }
}