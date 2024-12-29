using LeTrack.Jobs;
using Quartz;
using Quartz.AspNetCore;

namespace LeTrack.Extensions;

public static class JobExtensions
{
    public static IServiceCollection ConfigureJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.ScheduleJob<TriggerJob>(t =>
            {
                t.WithIdentity("TriggerJob", "Trigger");
                t.StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(30)));
                t.WithSimpleSchedule(s => s.WithIntervalInSeconds(53).RepeatForever());
            });
        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = false;
        });

        services.AddTransient<TriggerJob>();
        services.AddTransient<RaceCloseJob>();
        return services;
    }
}