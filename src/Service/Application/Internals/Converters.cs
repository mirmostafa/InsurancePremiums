using Service.Application.DataSources;
using Service.Domain.Entities;

namespace Service.Application.Internals;

internal static class Converters
{
    public static CoverageDto? ToDto(this Coverage? coverage) =>
        coverage == null?null: new(coverage.Name, coverage.Rate, coverage.Id);

    public static IEnumerable<CoverageDto?> ToDto(this IEnumerable<Coverage> coverages) =>
        coverages.Select(ToDto);
}