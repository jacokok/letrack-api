using LeTrack.Data;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Test;

public class Endpoint : EndpointWithoutRequest<bool>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        await _dbContext.Event.ToListAsync();
        await SendAsync(true);
    }
}