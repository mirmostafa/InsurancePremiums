using System.Diagnostics.CodeAnalysis;

using Service.Application.DataSources;
using Service.Domain.Dtos;

namespace Service.Application.Internals;

internal static class Converters
{
    [return: NotNullIfNotNull(nameof(coverage))]
    public static CoverageDto? ToDto(this Coverage? coverage) =>
        coverage == null ? null : new(coverage.Name, coverage.Rate, coverage.Id, coverage.InvestmentMin, coverage.InvestmentMax);

    public static IEnumerable<CoverageDto?> ToDto(this IEnumerable<Coverage> coverages) =>
        coverages.Select(ToDto);
}