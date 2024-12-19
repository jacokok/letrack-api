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

        var lastEvent = await _dbContext.Event
            .Where(x => x.TrackId == eventModel.TrackId && x.Timestamp < eventModel.Timestamp)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(ct);

        var race = await _dbContext.Race.FirstOrDefaultAsync(x => x.IsActive && x.RaceTracks.Any(x => x.TrackId == eventModel.TrackId), ct);

        TimeSpan? lapTimeSpan = (lastEvent != null) ? eventModel.Timestamp - lastEvent.Timestamp : null;
        TimeSpan? lapDifference = null;

        if (lastEvent != null)
        {
            var lastLap = await _dbContext.Lap.FirstOrDefaultAsync(x => x.Id == lastEvent.Id);
            if (lastLap != null)
            {
                lapDifference = lastLap.LapTime - lapTimeSpan;
            }
        }

        // TODO: Flag lap if something is fishy

        var lap = new Lap
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId,
            Timestamp = eventModel.Timestamp,
            LastLapId = lastEvent?.Id,
            LapTime = lapTimeSpan,
            LapTimeDifference = lapDifference,
            IsFlagged = lapTimeSpan == null,
            RaceId = race?.Id ?? 0
        };
        await _dbContext.Lap.AddAsync(lap);
        await _dbContext.SaveChangesAsync(ct);
        return;
    }
}
