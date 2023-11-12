namespace Service.Application.Internals;

internal static class Calculator
{
    // Function to calculate the net insurance cost for a specific coverage
    internal static double CalculateInsuranceCost(double rate, int capital) =>
        rate * capital;

    // Function to calculate the total net insurance cost for multiple coverages
    internal static double CalculateTotalInsuranceCost(IEnumerable<(double Rate, int Capitals)> data) =>
        data.Sum(dataItem => CalculateInsuranceCost(dataItem.Rate, dataItem.Capitals));
}