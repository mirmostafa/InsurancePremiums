namespace DomainEntities;

public abstract class CoverageTypeBase : ValueObject
{
    internal CoverageTypeBase(CoverageTypeEnum value, double rate)
    {
        this.Value = value;
        this.Rate = rate;
    }

    public double Rate { get; }

    internal CoverageTypeEnum Value { get; }
}

public abstract class Entity
{
    protected Entity(Guid id = default)
    {
    }

    public Guid Id { get; }
}

public abstract class ValueObject
{
}

// Define the coverage types
internal enum CoverageTypeEnum
{
    Surgery,
    Dental,
    Hospitalization
}