namespace LeTrack.Entities;

public class Race
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<RaceTrack> RaceTracks { get; set; } = new();
    public bool IsActive { get; set; }
}