using LeTrack.Data;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Players.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/players/{id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var player = await _dbContext.Player.FirstOrDefaultAsync(x => x.Id == req.Id, ct);
        if (player == null)
        {
            throw new Exception("Player not found");
        }

        _dbContext.Player.Remove(player);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}