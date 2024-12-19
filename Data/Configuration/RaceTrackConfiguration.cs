using LeTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeTrack.Data.Configuration;

public class RaceTrackConfiguration : IEntityTypeConfiguration<RaceTrack>
{
    public void Configure(EntityTypeBuilder<RaceTrack> builder)
    {
        builder.HasKey(k => new { k.RaceId, k.TrackId });
    }
}