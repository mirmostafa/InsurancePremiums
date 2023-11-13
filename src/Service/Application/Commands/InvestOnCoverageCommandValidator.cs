using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Cqrs.Models.Commands;

using Service.Domain.Entities;

namespace Service.Application.Commands;
internal sealed class InvestOnCoverageCommandValidator : ICommandValidator<InsertInvestOnCoverageCommandParams>
{
    public ValueTask ValidateAsync(InsertInvestOnCoverageCommandParams command)
    {
        Checker.MustBeArgumentNotNull(command);

        return ValueTask.CompletedTask;
    }
}
