using LeTrack.Data;
using LeTrack.DTO;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Race.Summary;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/race/summary/{raceId}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        Response response = new();

        var race = await _dbContext.Race
            .Include(x => x.RaceTracks)
                .ThenInclude(x => x.Player)
            .FirstOrDefaultAsync(x => x.Id == r.RaceId, ct);

        if (race == null)
        {
            throw new Exception("Race not found");
        }

        List<LapDTO>? laps = await _dbContext.Lap.Where(x => x.RaceId == r.RaceId).OrderByDescending(x => x.Timestamp).ProjectToDto().ToListAsync(ct);

        foreach (var track in race.RaceTracks)
        {
            var trackLapsRaw = laps.Where(x => x.TrackId == track.TrackId).ToList();
            var trackLaps = trackLapsRaw.Select((x, index) => { x.LapNumber = trackLapsRaw.Count - index; return x; }).ToList();
            response.Tracks.Add(new Track
            {
                TrackId = track.TrackId,
                Laps = trackLaps,
                TotalLaps = trackLaps.Count,
                FastestLap = trackLaps.Where(x => !x.IsFlagged).OrderBy(x => x.LapTime).FirstOrDefault(),
                Player = track.Player
            });
        }

        response.TotalLaps = laps.Count;
        response.FastestLap = laps.Where(x => !x.IsFlagged).OrderBy(x => x.LapTime).FirstOrDefault();
        response.Race = race;

        await SendAsync(response);
    }
}