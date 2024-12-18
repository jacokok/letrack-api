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
        // TODO: Save lap times.
        // Find last lap event
        var lastEvent = await _dbContext.Event
            .Where(x => x.TrackId == eventModel.TrackId && x.Timestamp < eventModel.Timestamp)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(ct);

        TimeSpan? lapTimeSpan = (lastEvent != null) ? eventModel.Timestamp - lastEvent.Timestamp : null;

        // TODO: Check is Flagged
        // Save lap
        var lap = new Lap
        {
            Id = eventModel.Id,
            TrackId = eventModel.TrackId,
            Timestamp = eventModel.Timestamp,
            LastLapId = lastEvent.Id,
            LapTime = lapTimeSpan,
            IsFlagged = lapTimeSpan == null
        };
        await _dbContext.Lap.AddAsync(lap);
        await _dbContext.SaveChangesAsync(ct);
        return;
    }
}
