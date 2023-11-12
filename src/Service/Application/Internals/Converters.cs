using Service.Application.DataSources;
using Service.Domain.Dtos;

namespace Service.Application.Internals;

internal static class Converters
{
    public static CoverageDto ToDto(this Coverage coverage) =>
        new(coverage.Name, coverage.Rate, coverage.Id);

    public static IEnumerable<CoverageDto> ToDto(this IEnumerable<Coverage> coverages) =>
        coverages.Select(ToDto);
}