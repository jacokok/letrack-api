using LeTrack.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl.Matchers;

namespace LeTrack.Jobs;

public class TriggerJob : IJob
{
    private readonly ILogger<TriggerJob> _logger;
    private readonly AppDbContext _dbContext;
    private readonly ISchedulerFactory _schedulerFactory;

    public TriggerJob(AppDbContext dbContext, ILogger<TriggerJob> logger, ISchedulerFactory schedulerFactory)
    {
        _dbContext = dbContext;
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var raceTriggers = await _dbContext.Race.Where(x => x.IsActive && x.EndDateTime != null).ToListAsync(context.CancellationToken);

        IScheduler scheduler = await _schedulerFactory.GetScheduler(context.CancellationToken);

        foreach (var race in raceTriggers)
        {
            JobKey jobKey = new($"RaceCloseJob:{race.Id}", "RaceCloseJob");

            var triggers = await scheduler.GetTriggersOfJob(jobKey, context.CancellationToken);
            foreach (var curTrigger in triggers)
            {
                if (curTrigger.GetNextFireTimeUtc() == race.EndDateTime!.Value.ToUniversalTime())
                {
                    _logger.LogDebug("Job Trigger already there and happy {Scheduled} {Now}", curTrigger.GetNextFireTimeUtc(), DateTime.UtcNow);
                    return;
                }
                else
                {
                    _logger.LogDebug("Job Trigger not good all shiet");
                    await scheduler.DeleteJob(jobKey, context.CancellationToken);
                }
            }

            var job = JobBuilder.Create<RaceCloseJob>()
                        .WithIdentity(jobKey)
                        .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"RaceCloseJob:{race.Id}", "RaceCloseJob")
                .StartAt(race.EndDateTime!.Value.ToUniversalTime())
                .Build();

            await scheduler.ScheduleJob(job, trigger, context.CancellationToken);
            _logger.LogDebug("Triggered {race} {at}", race.Name, race.EndDateTime.Value.ToUniversalTime());

        }
    }
}