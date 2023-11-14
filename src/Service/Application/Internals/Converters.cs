using System.Diagnostics.CodeAnalysis;

using Service.Application.DataSources;
using Service.Domain.Dtos;
using Service.Domain.ValueObjects;

namespace Service.Application.Internals;

internal static class Converters
{
    [return: NotNullIfNotNull(nameof(coverage))]
    public static CoverageDto? ToDto(this Coverage? coverage) =>
        coverage == null ? null : new(coverage.Id, coverage.Name, Rate.Create(coverage.Rate), coverage.InvestmentMin, coverage.InvestmentMax);

    public static IEnumerable<CoverageDto?> ToDto(this IEnumerable<Coverage> coverages) =>
        coverages.Select(ToDto);
}