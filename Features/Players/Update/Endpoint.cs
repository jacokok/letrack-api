using LeTrack.Data;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Players.Update;

public class Endpoint : Endpoint<Request, Player>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/players");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var player = await _dbContext.Player.FirstOrDefaultAsync(x => x.Id == req.Id, ct);
        if (player == null)
        {
            throw new Exception("Player not found");
        }


        player.NickName = req.NickName;
        player.Name = req.Name;

        await _dbContext.SaveChangesAsync();
        await SendAsync(player);
    }
}