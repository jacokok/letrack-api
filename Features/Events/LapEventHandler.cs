using LeTrack.Data;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Features.Events;

public class LapEventHandler : IEventHandler<LapEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LapEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(LapEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var _dbContext = scope.Resolve<AppDbContext>();

        if (_dbContext == null)
        {
            throw new Exception("Dependency injection failed");
        }

        var race = await _dbContext.Race.FirstOrDefaultAsync(x => x.IsActive && x.RaceTracks.Any(x => x.TrackId == eventModel.TrackId), ct);


        TimeSpan? lapTimeSpan = null;
        TimeSpan? lapDifference = null;
        bool isFlagged = false;
        string? flagReason = null;

        int raceId = race?.Id ?? 0;

        var lastLap = await _dbContext.Lap
            .Where(x => x.TrackId == eventModel.TrackId && x.Timestamp < eventModel.Timestamp && x.RaceId == raceId)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(ct);

        Guid? lastLapId = lastLap?.Id;
        if (lastLap != null)
        {

            lapTimeSpan = eventModel.Timestamp - lastLap.Timestamp;
            if (race?.RestartDateTime != null)
            {
                if (race.RestartDateTime > lastLap.Timestamp)
                {
                    lapTimeSpan = eventModel.Timestamp - race.RestartDateTime;
                    isFlagged = true;
                    flagReason = "First lap or race restart";

                }
            }

            lapDifference = lastLap.LapTime - lapTimeSpan;
        }
        else
        {
            if (race != null)
            {
                lapTimeSpan = eventModel.Timestamp - race.RestartDateTime;
                isFlagged = true;
                flagReason = "First lap or race restart";
            }
        }

        if (lapTimeSpan == null)
        {
            isFlagged = true;
            flagReason = "No time span";
        }

        // TODO: Flag lap if something is fishy

        var lap = new Lap
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId,
            Timestamp = eventModel.Timestamp,
            LastLapId = lastLapId,
            LapTime = lapTimeSpan,
            LapTimeDifference = lapDifference,
            IsFlagged = isFlagged,
            RaceId = race?.Id ?? 0,
            FlagReason = flagReason
        };
        await _dbContext.Lap.AddAsync(lap);
        await _dbContext.SaveChangesAsync(ct);
        return;
    }
}
