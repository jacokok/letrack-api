namespace LeTrack.Features.Race.Update;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<int> Players { get; set; } = new();
}