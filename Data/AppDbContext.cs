using System.Reflection;
using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeTrack.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Event> Event => Set<Event>();
    public DbSet<Lap> Lap => Set<Lap>();
    public DbSet<Player> Player => Set<Player>();
    public DbSet<Race> Race => Set<Race>();
    public DbSet<RaceTrack> RaceTrack => Set<RaceTrack>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}