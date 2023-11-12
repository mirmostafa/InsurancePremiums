namespace Service.Domain.Dtos;

public sealed record CoverageDto(string Name, decimal Rate, Guid Id)
{
}