using Kayord.Pos.Common.Extensions;
using LeTrack.Data;
using LeTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Race.List;

public class Endpoint : Endpoint<Request, PaginatedList<Entities.Race>>
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
            .OrderByDescending(x => x.CreatedDateTime)
            .GetPagedAsync(req, ct);

        await SendAsync(results);
    }
}