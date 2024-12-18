using LeTrack.Data;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Track.Summary;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/track/summary/{trackId}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        Response response = new();
        var laps = await _dbContext.Lap.Where(x => x.TrackId == r.TrackId).ToListAsync(ct);
        response.TotalLaps = laps.Count;
        response.Last10Laps = laps.OrderByDescending(x => x.Timestamp).Take(10).ToList();
        response.FastestLap = laps.Where(x => !x.IsFlagged).OrderBy(x => x.LapTime).FirstOrDefault();
        response.Laps = laps;
        await SendAsync(response);
    }
}