﻿namespace Service.Infrastructure.Bcl.Results;

public sealed class TryMethodResult
    : ResultBase
{
    private TryMethodResult(
    in bool? succeed = null,
    in object? status = null,
    in string? message = null,
    in IEnumerable<(object Id, object Error)>? errors = null,
    in IEnumerable<(string Id, object Data)>? ExtraData = null)
    : base(succeed, status, message, errors, ExtraData) { }

    private TryMethodResult(
    in Exception exception,
    in string? message = null,
    in IEnumerable<(object Id, object Error)>? errors = null,
    in IEnumerable<(string Id, object Data)>? ExtraData = null)
    : base(false, exception, message, errors, ExtraData) { }

    /// <summary>
    /// Creates a new TryMethodResult with a given value, status, message, errors, and extra data.
    /// </summary>
    /// <param name="value">The value of the TryMethodResult.</param>
    /// <param name="status">The status of the TryMethodResult.</param>
    /// <param name="message">The message of the TryMethodResult.</param>
    /// <param name="errors">The errors of the TryMethodResult.</param>
    /// <param name="extraData">The extra data of the TryMethodResult.</param>
    /// <returns>
    /// A new TryMethodResult with the given value, status, message, errors, and extra data.
    /// </returns>
    public static TryMethodResult CreateFailure(
        in object? status = null,
        in string? message = null,
        in IEnumerable<(object Id, object Error)>? errors = null,
        in IEnumerable<(string Id, object Data)>? extraData = null)
        => new(false, status, message, errors, extraData);

    public static TryMethodResult CreateFailure(
        in Exception exception,
        in string? message = null,
        in IEnumerable<(object Id, object Error)>? errors = null,
        in IEnumerable<(string Id, object Data)>? extraData = null)
        => new(exception, message, errors, extraData);

    /// <summary>
    /// Creates a new TryMethodResult with the given value, status, message, errors, and extraData.
    /// </summary>
    /// <param name="value">The value of the TryMethodResult.</param>
    /// <param name="status">The status of the TryMethodResult.</param>
    /// <param name="message">The message of the TryMethodResult.</param>
    /// <param name="errors">The errors of the TryMethodResult.</param>
    /// <param name="extraData">The extraData of the TryMethodResult.</param>
    /// <returns>A new TryMethodResult with the given value, status, message, errors, and extraData.</returns>
    public static TryMethodResult CreateSuccess(
        in object? status = null,
        in string? message = null,
        in IEnumerable<(object Id, object Error)>? errors = null,
        in IEnumerable<(string Id, object Data)>? extraData = null) =>
        new(true, status, message, errors, extraData);

    public static explicit operator bool(TryMethodResult result) =>
        result.IsSucceed;

    public static TryMethodResult New(bool? succeed, string? message = null) =>
        new(succeed, message: message);

    /// <summary>
    /// Creates a new TryMethodResult object with the specified value, success status, and optional message.
    /// </summary>
    public static TryMethodResult TryParseResult(bool? succeed, string? message = null) =>
        New(succeed, message: message);
}

public sealed class TryMethodResult<TValue>(in TValue? value,
    in bool? succeed = null,
    in object? status = null,
    in string? message = null,
    in IEnumerable<(object Id, object Error)>? errors = null,
    in IEnumerable<(string Id, object Data)>? ExtraData = null)
    : Result<TValue?>(value, succeed, status, message, errors, ExtraData)
{
    /// <summary>
    /// Creates a new TryMethodResult with a given value, status, message, errors, and extra data.
    /// </summary>
    /// <param name="value">The value of the TryMethodResult.</param>
    /// <param name="status">The status of the TryMethodResult.</param>
    /// <param name="message">The message of the TryMethodResult.</param>
    /// <param name="errors">The errors of the TryMethodResult.</param>
    /// <param name="extraData">The extra data of the TryMethodResult.</param>
    /// <returns>
    /// A new TryMethodResult with the given value, status, message, errors, and extra data.
    /// </returns>
    public static new TryMethodResult<TValue?> CreateFailure(in TValue? value = default,
        in object? status = null,
        in string? message = null,
        in IEnumerable<(object Id, object Error)>? errors = null,
        in IEnumerable<(string Id, object Data)>? extraData = null)
        => new(value, false, status, message, errors, extraData);

    /// <summary>
    /// Creates a new TryMethodResult with the given value, status, message, errors, and extraData.
    /// </summary>
    /// <param name="value">The value of the TryMethodResult.</param>
    /// <param name="status">The status of the TryMethodResult.</param>
    /// <param name="message">The message of the TryMethodResult.</param>
    /// <param name="errors">The errors of the TryMethodResult.</param>
    /// <param name="extraData">The extraData of the TryMethodResult.</param>
    /// <returns>A new TryMethodResult with the given value, status, message, errors, and extraData.</returns>
    public static new TryMethodResult<TValue> CreateSuccess(in TValue value,
        in object? status = null,
        in string? message = null,
        in IEnumerable<(object Id, object Error)>? errors = null,
        in IEnumerable<(string Id, object Data)>? extraData = null) =>
        new(value, true, status, message, errors, extraData);

    public static explicit operator bool(TryMethodResult<TValue?> result) =>
        result.IsSucceed;

    public static explicit operator TValue?(TryMethodResult<TValue> result) =>
        result.Value;

    /// <summary>
    /// Creates a new TryMethodResult object with the specified value, success status, and optional message.
    /// </summary>
    public static TryMethodResult<TValue> TryParseResult(bool? succeed, TValue? value, string? message = null) =>
        new(value, succeed, message: message);
}