using LeTrack.Data;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Race.Start;

public class Endpoint : Endpoint<Request, Entities.Race>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/race/start");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.Race? race = await _dbContext.Race.FirstOrDefaultAsync(x => x.Id == req.Id, ct);
        if (race == null)
        {
            throw new Exception("Race not found");
        }

        var now = DateTime.UtcNow;

        race.IsActive = true;
        if (race.TimeRemaining != null)
        {
            race.EndDateTime = now + race.TimeRemaining;
        }
        else if (req.Duration != 0)
        {
            race.EndDateTime = now + TimeSpan.FromMinutes(req.Duration);
        }
        else
        {
            race.EndDateTime = null;
        }

        if (req.Laps != 0)
        {
            race.EndLapCount = req.Laps;
        }
        else
        {
            race.EndLapCount = null;
        }

        race.TimeRemaining = null;

        if (race.StartDateTime == null)
        {
            race.StartDateTime = now;
        }
        race.RestartDateTime = now;

        await _dbContext.SaveChangesAsync();
        await SendAsync(race);
    }
}