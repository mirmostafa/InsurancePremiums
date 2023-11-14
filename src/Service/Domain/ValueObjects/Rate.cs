using RateType = System.Decimal;

namespace Service.Domain.ValueObjects;

public sealed class Rate : ValueObject<Rate>
{
    private readonly RateType _value;

    private Rate(RateType value) =>
        this._value = value;

    public static Rate Default { get; } = FromPrimitiveType(0);

    public static Rate Create(RateType rate) =>
        new(rate);

    public static explicit operator Rate(RateType rate) =>
        Create(rate);

    public static explicit operator RateType(Rate rate) =>
        rate._value;

    public static Rate FromPrimitiveType(RateType rate) =>
                Create(rate);

    public RateType ToPrimitiveType() =>
        this._value;

    protected override int OnGetHashCode() =>
        this._value.GetHashCode();
}