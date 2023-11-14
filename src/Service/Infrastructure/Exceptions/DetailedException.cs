namespace Service.Infrastructure.Exceptions;

public abstract class DetailedException : ExceptionBase
{
    protected DetailedException()
    {
    }

    protected DetailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DetailedException(string message, string? instruction = null, string? title = null, string? details = null, Exception? inner = null, object? owner = null)
        : base(message, inner)
    {
        this.Instruction = instruction;
        this.Owner = owner;
        this.Title = title;
        this.Details = details;
    }

    /// <summary>
    /// Gets or sets the details.
    /// </summary>
    /// <value>The details.</value>
    public string? Details { get; init; }

    /// <summary>
    /// Gets the instruction.
    /// </summary>
    /// <value>The instruction.</value>
    public string? Instruction { get; init; }

    /// <summary>
    /// Gets or sets the owner.
    /// </summary>
    /// <value>The owner.</value>
    public object? Owner { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <value>The title.</value>
    public string? Title { get; init; }
}