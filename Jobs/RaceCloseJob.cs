using LeTrack.Data;
using LeTrack.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace LeTrack.Jobs;

public class RaceCloseJob : IJob
{
    private readonly ILogger<RaceCloseJob> _logger;
    private readonly IHubContext<LeTrackHub, ILeTrackHub> _hub;
    private readonly AppDbContext _dbContext;

    public RaceCloseJob(AppDbContext dbContext, ILogger<RaceCloseJob> logger, IHubContext<LeTrackHub, ILeTrackHub> hub)
    {
        _dbContext = dbContext;
        _logger = logger;
        _hub = hub;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogDebug("Auto closing race based on end date");
        var raceTriggers = await _dbContext.Race.Where(x => x.IsActive && x.EndDateTime <= DateTime.UtcNow).ToListAsync();
        foreach (var race in raceTriggers)
        {
            race.IsActive = false;
            await _hub.Clients.All.RaceComplete(race.Id);
        }
        await _dbContext.SaveChangesAsync();
    }
}