namespace Service.Domain.Dtos;

public sealed record CoverageDto(Guid Id, string Name, decimal Rate, long? InvestmentMin, long? InvestmentMax);

public sealed record InvestmentRequestDto(Guid Id, string Title, DateTime CreateDate, IEnumerable<InvestmentValueDto> Values);

public sealed record InvestmentValueDto(Guid Id, Guid RequestId, Guid CoverageId, decimal Value);