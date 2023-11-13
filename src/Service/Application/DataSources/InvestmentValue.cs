using System;
using System.Collections.Generic;

namespace Service.Application.DataSources;

public partial class InvestmentValue
{
    public Guid Id { get; set; }

    public Guid InvestmentRequestId { get; set; }

    public Guid CoverageId { get; set; }

    public decimal Value { get; set; }

    public virtual Coverage Coverage { get; set; } = null!;

    public virtual InvestmentRequest InvestmentRequest { get; set; } = null!;
}
