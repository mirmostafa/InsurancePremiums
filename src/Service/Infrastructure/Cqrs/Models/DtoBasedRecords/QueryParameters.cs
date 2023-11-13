using Service.Infrastructure.Cqrs.Models.Commands;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Infrastructure.Cqrs.Models.DtoBasedRecords;

[Obsolete("To find where this is used.", true)]
public readonly record struct DtoQuery<TParamDto, TResultDto>(TParamDto Dto) : IQuery<TResultDto>;
[Obsolete("To find where this is used.", true)]
public readonly record struct DtoCommand<TParamDto, TResultDto>(TParamDto Dto) : ICommand;