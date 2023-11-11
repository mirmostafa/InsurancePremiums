namespace DomainEntities;

public sealed class Coverage : Entity
{
    private Coverage(CoverageTypeBase coverageType, Guid id = default) : base(id) =>
        this.CoverageType = coverageType;

    public CoverageTypeBase CoverageType { get; }

    public static Coverage Create(CoverageTypeBase coverageType, Guid id = default) =>
        new(coverageType);
}