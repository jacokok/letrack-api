using LeTrack.Data;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Race.Stop;

public class Endpoint : Endpoint<Request, Entities.Race>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/race/stop");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.Race? race = await _dbContext.Race.FirstOrDefaultAsync(x => x.Id == req.Id, ct);
        if (race == null)
        {
            throw new Exception("Race not found");
        }

        race.IsActive = false;

        if (race.EndDateTime != null)
        {
            race.TimeRemaining = race.EndDateTime - DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
        await SendAsync(race);
    }
}