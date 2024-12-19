using LeTrack.Entities;

namespace LeTrack.Features.Race.Summary;

public class Response
{
    public List<LapDTO> Laps { get; set; } = new();
    public LapDTO? FastestLap { get; set; }
    public int TotalLaps { get; set; }
    public Entities.Race Race { get; set; } = default!;
}