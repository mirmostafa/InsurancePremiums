using System.Collections;

namespace Service.Domain.ValueObjects;

public sealed class CoverageInvestment(Guid coverageId, decimal value) : ValueObject<CoverageInvestment>
{
    public static CoverageInvestment Default { get; } = new(Guid.Empty, 0);

    public Guid CoverageId { get; } = coverageId;

    public decimal Value { get; } = value;

    public void Deconstruct(out Guid coverageId, out decimal value) =>
        (coverageId, value) = (this.CoverageId, this.Value);

    protected override int OnGetHashCode() =>
        HashCode.Combine(this.CoverageId.GetHashCode(), this.Value.GetHashCode());
}

public sealed class CoverageInvestments() : ValueObject<CoverageInvestments>, IEnumerable<CoverageInvestment>
{
    public CoverageInvestment[] Investments { get; } = new CoverageInvestment[3];

    public IEnumerator<CoverageInvestment> GetEnumerator() =>
        ((IEnumerable<CoverageInvestment>)this.Investments).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.Investments.GetEnumerator();

    internal bool IsValid() =>
        this.Investments[0] != CoverageInvestment.Default;

    protected override int OnGetHashCode() =>
        HashCode.Combine(
            this.Investments[0] ?? CoverageInvestment.Default,
            this.Investments[1] ?? CoverageInvestment.Default,
            this.Investments[2] ?? CoverageInvestment.Default);
}