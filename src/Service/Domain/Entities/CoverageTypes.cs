namespace DomainEntities;

public sealed class Dental : CoverageTypeBase
{
    public Dental() : base(CoverageTypeEnum.Dental, 0.0042)
    {
    }
}

public sealed class Hospitalization : CoverageTypeBase
{
    public Hospitalization() : base(CoverageTypeEnum.Hospitalization, 0.005)
    {
    }
}

public sealed class Surgery : CoverageTypeBase
{
    public Surgery() : base(CoverageTypeEnum.Surgery, 0.0052)
    {
    }
}