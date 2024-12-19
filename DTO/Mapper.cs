using LeTrack.Entities;
using Riok.Mapperly.Abstractions;

namespace LeTrack.DTO;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<LapDTO> ProjectToDto(this IQueryable<Lap> q);

    [MapperIgnoreTarget(nameof(LapDTO.LapNumber))]
    private static partial LapDTO MapLap(Lap model);
}