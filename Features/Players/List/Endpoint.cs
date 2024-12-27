using LeTrack.Data;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Players.List;

public class Endpoint : EndpointWithoutRequest<List<Entities.Player>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/players");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Player.OrderBy(x => x.Name).ToListAsync(ct);
        await SendAsync(results);
    }
}