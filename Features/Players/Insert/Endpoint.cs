using LeTrack.Data;
using LeTrack.Entities;

namespace LeTrack.Features.Players.Insert;

public class Endpoint : Endpoint<Request, Player>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/players");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Player player = new() { Name = req.Name, NickName = req.NickName };
        await _dbContext.Player.AddAsync(player);
        await _dbContext.SaveChangesAsync();
        await SendAsync(player);
    }
}