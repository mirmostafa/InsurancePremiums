using System;
using System.Collections.Generic;

namespace Service.Application.DataSources;

public partial class InvestmentRequest
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual ICollection<InvestmentValue> InvestmentValues { get; set; } = new List<InvestmentValue>();
}
