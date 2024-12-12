using LeTrack.Data;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.BackgroundServices;

public class Migrator : IHostedService
{
    private readonly ILogger<Migrator> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public Migrator(IServiceScopeFactory serviceScopeFactory, ILogger<Migrator> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (context.Database.IsNpgsql())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}