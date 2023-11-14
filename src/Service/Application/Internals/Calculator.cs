namespace Service.Application.Internals;

internal static class Calculator
{
    // Function to calculate the net insurance cost for a specific coverage
    internal static decimal CalculateInsuranceCost(decimal rate, decimal capital) =>
        rate * capital;

    // Function to calculate the total net insurance cost for multiple coverages
    internal static decimal CalculateTotalInsuranceCost(IEnumerable<(decimal Rate, decimal Capitals)> data) =>
        data.Sum(dataItem => CalculateInsuranceCost(dataItem.Rate, dataItem.Capitals));
}