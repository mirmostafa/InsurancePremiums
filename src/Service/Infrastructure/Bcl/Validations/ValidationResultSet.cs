﻿#nullable disable

using System.ComponentModel;
using System.Diagnostics;

using Service.Infrastructure.Bcl.Helpers;
using Service.Infrastructure.Bcl.Results;

namespace Service.Infrastructure.Bcl.Validations;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[DebuggerStepThrough]
[StackTraceHidden]
public sealed class ValidationResultSet<TValue>(TValue value, CheckBehavior behavior, string valueName)
{
    internal readonly CheckBehavior Behavior = behavior;
    internal readonly List<(Func<TValue, bool> IsValid, Func<Exception> OnError)> RuleList = new();
    internal readonly string _valueName = valueName;

    public IEnumerable<(Func<TValue, bool> IsValid, Func<Exception> OnError)> Rules =>
        this.RuleList.Iterate();

    public TValue Value { get; } = value;

    public static implicit operator Result<TValue>(ValidationResultSet<TValue> source) =>
        source.Build();

    public static implicit operator TValue(ValidationResultSet<TValue> source) =>
        source.Value;
}

/// <summary>
/// Enum to define the behavior when checking a condition.
/// </summary>
public enum CheckBehavior
{
    GatherAll,
    ReturnFirstFailure,
    ThrowOnFail,
}