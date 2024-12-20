using LeTrack.Data;
using LeTrack.DTO;
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

        var race = await _dbContext.Race
            .Include(x => x.RaceTracks)
                .ThenInclude(x => x.Player)
            .FirstOrDefaultAsync(x => x.IsActive && x.RaceTracks.Any(x => x.TrackId == r.TrackId), ct);

        List<LapDTO>? laps = null;
        if (race == null)
        {
            laps = await _dbContext.Lap.Where(x => x.TrackId == r.TrackId).OrderByDescending(x => x.Timestamp).Take(10).ProjectToDto().ToListAsync(ct);
        }
        else
        {
            laps = await _dbContext.Lap.Where(x => x.TrackId == r.TrackId && x.RaceId == race.Id).OrderByDescending(x => x.Timestamp).ProjectToDto().ToListAsync(ct);
        }

        // Add Lap Number
        laps = laps.Select((x, index) => { x.LapNumber = laps.Count - index; return x; }).ToList();
        response.TotalLaps = (race == null) ? 0 : laps.Count;
        response.Last10Laps = laps.OrderByDescending(x => x.Timestamp).Take(10).ToList();
        response.FastestLap = laps.Where(x => !x.IsFlagged).OrderBy(x => x.LapTime).FirstOrDefault();
        response.Race = race;

        await SendAsync(response);
    }
}