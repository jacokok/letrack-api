using LeTrack.Data;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Race.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/race/{id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var race = await _dbContext.Race.FirstOrDefaultAsync(x => x.Id == req.Id, ct);
        if (race == null)
        {
            throw new Exception("Race not found");
        }

        _dbContext.Race.Remove(race);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}