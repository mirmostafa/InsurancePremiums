using Infrastructure.Cqrs.Models.Commands;
using Infrastructure.Cqrs.Models.Queries;

namespace Infrastructure.Cqrs.Models.DtoBasedRecords;

[Obsolete("To find where this is used.", true)]
public readonly record struct DtoQuery<TParamDto, TResultDto>(TParamDto Dto) : IQuery<TResultDto>;
[Obsolete("To find where this is used.", true)]
public readonly record struct DtoCommand<TParamDto, TResultDto>(TParamDto Dto) : ICommand;