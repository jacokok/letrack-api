using LeTrack.Data;
using LeTrack.Entities;

namespace LeTrack.Features.Race.Insert;

public class Endpoint : Endpoint<Request, Entities.Race>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/race");
        AllowAnonymous();
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Race race = new() { Name = req.Name, EndDateTime = req.EndDateTime, StartDateTime = req.StartDateTime, EndLapCount = req.EndLapCount, IsActive = req.IsActive };
        await _dbContext.Race.AddAsync(race);
        await _dbContext.SaveChangesAsync();
        await SendAsync(race);
    }
}