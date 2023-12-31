﻿using System;
using System.Collections.Generic;

namespace Service.Application.DataSources;

public partial class Coverage
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public long? InvestmentMin { get; set; }

    public long? InvestmentMax { get; set; }

    public virtual ICollection<InvestmentValue> InvestmentValues { get; set; } = new List<InvestmentValue>();
}
