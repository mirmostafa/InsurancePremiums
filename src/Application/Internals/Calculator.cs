using DomainEntities;

namespace Services.Internals;

internal static class Calculator
{
    // Function to calculate the net insurance cost for a specific coverage
    internal static double CalculateInsuranceCost(CoverageTypeBase coverage, int capital) =>
        coverage.Rate * capital;

    // Function to calculate the total net insurance cost for multiple coverages
    internal static double CalculateTotalInsuranceCost(IEnumerable<(CoverageTypeBase CoverageType, int Capitals)> data) =>
        data.Sum(dataItem => CalculateInsuranceCost(dataItem.CoverageType, dataItem.Capitals));
}