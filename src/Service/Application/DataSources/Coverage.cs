using System;
using System.Collections.Generic;

namespace Service.Application.DataSources;

public partial class Coverage
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }
}
