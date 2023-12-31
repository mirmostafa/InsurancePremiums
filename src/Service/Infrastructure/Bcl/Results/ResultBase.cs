﻿using System.Diagnostics;
using System.Text;

using Service.Infrastructure.Bcl.Helpers;
using Service.Infrastructure.Bcl.Validations;

namespace Service.Infrastructure.Bcl.Results;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public abstract class ResultBase(
    in bool? succeed = null,
    in object? status = null,
    in string? message = null,
    in IEnumerable<(object Id, object Error)>? errors = null,
    in IEnumerable<(string, object)>? extraData = null)
{
    private readonly IEnumerable<(object Id, object Error)>? _error;

    private IEnumerable<(object Id, object Error)>? _errors = errors;

    private IEnumerable<(string, object)>? _extraData = extraData;

    public IEnumerable<(object Id, object Error)> Errors { get => this._errors ??= Enumerable.Empty<(object, object)>(); init => this._errors = value; }

    public IEnumerable<(string Id, object Data)> ExtraData => this._extraData ??= Enumerable.Empty<(string, object)>();

    /// <summary>
    /// Gets a value indicating whether the operation has failed.
    /// </summary>
    public virtual bool IsFailure => !this.IsSucceed;

    /// <summary>
    /// Checks if the operation was successful by checking the Succeed flag, Status and Errors.
    /// </summary>
    public virtual bool IsSucceed => this.Succeed ?? this.Status is null or 0 or 200 && (!this.Errors?.Any() ?? true);

    public string? Message { get; set; } = message;

    public object? Status { get; set; } = status;

    public bool? Succeed { get; set; } = succeed;

    public static implicit operator bool(ResultBase result) =>
        result.NotNull().IsSucceed;

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    public virtual bool Equals(ResultBase? other) =>
        other is not null && this.Status == other.Status;

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>The hash code for the current instance.</returns>
    public override int GetHashCode() =>
        HashCode.Combine(this.Status, this.Message);

    /// <summary>
    /// Generates a string representation of the result object.
    /// </summary>
    /// <returns>A string representation of the result object.</returns>
    public override string ToString()
    {
        var result = new StringBuilder();
        if (!this.Message.IsNullOrEmpty())
        {
            _ = result.AppendLine(this.Message);
        }
        if (!(this.Status?.ToString()?.IsNullOrEmpty() ?? true))
        {
            _ = result.AppendLine(this.Status.ToString());
        }
        else if (this.Errors?.Count() == 1)
        {
            _ = result.AppendLine(this.Errors!.First().Error?.ToString() ?? "An error occurred.");
        }
        else if (this.Errors?.Any() ?? false)
        {
            foreach (var errorMessage in this.Errors.Select(x => x.Error?.ToString()).Compact())
            {
                _ = result.AppendLine($"- {errorMessage}");
            }
        }
        if (result.Length == 0)
        {
            _ = result.AppendLine($"IsSucceed: {this.IsSucceed}");
        }

        return result.ToString();
    }

    /// <summary>
    /// Combines multiple ResultBase objects into a single ResultBase object.
    /// </summary>
    /// <param name="results">The ResultBase objects to combine.</param>
    /// <returns>A ResultBase object containing the combined results.</returns>
    protected static (bool? Succeed, object? Status, string? Message, IEnumerable<(object Id, object Error)>? Errors, IEnumerable<(string Id, object Data)>? ExtraData) Combine(params ResultBase[] results)
    {
        bool? isSucceed = results.All(x => x.Succeed == null) ? null : results.All(x => x.IsSucceed);
        var status = results.LastOrDefault(x => x.Status is not null)?.Status;
        var message = results.LastOrDefault(x => !x.Message.IsNullOrEmpty())?.Message;
        var errors = results.SelectMany(x => x?.Errors ?? Enumerable.Empty<(object Id, object Error)>());
        var statusBuffer = results.Where(x => x.Status is not null).Select(x => x.Status).ToList();
        if (statusBuffer.Count > 1)
        {
            errors = errors.AddRangeImmuted(statusBuffer.Select(x => ((object)null!, x!)));
            status = null;
        }
        var extraData = combineExtraData(results);

        return (isSucceed, status, message, errors, extraData);

        static IEnumerable<(string Id, object Data)> combineExtraData(in ResultBase[] results)
        {
            var lastData = results
                .Where(x => x?.ExtraData is not null)
                .SelectMany(x => x.ExtraData!)
                .Where(item => !item.Equals(default) && !item.Id.IsNullOrEmpty() && item.Data is not null);
            return !lastData.Any()
                ? results.Select(x => ("Previous Result", (object)x))
                : lastData;
        }
    }

    private string GetDebuggerDisplay() =>
        this.ToString();
}