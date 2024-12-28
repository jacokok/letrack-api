using LeTrack.Data;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Race.List;

public class Endpoint : Endpoint<Request, List<Entities.Race>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/race");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.Race
            .Include(x => x.RaceTracks)
                .ThenInclude(x => x.Player)
            .Where(x => x.IsActive == req.IsActive)
            .OrderByDescending(x => x.CreatedDateTime)
            .ToListAsync(ct);

        await SendAsync(results);
    }
}