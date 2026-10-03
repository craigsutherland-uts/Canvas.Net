using Canvas.Client.Entities;

using Microsoft.Extensions.Logging;

using System.Globalization;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="IAssignments"/>.
/// </summary>
/// <param name="parent">The parent <see cref="ICanvasClient"/>.</param>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
/// <param name="options">The <see cref="ICanvasOptions"/> to use when connecting to Canvas.</param>
[CanvasClient<IAssignments>]
public sealed class StandardAssignmentsClient(
        ICanvasClient parent,
        ICanvasConnection connection,
        ILogger<StandardAssignmentsClient> logger,
        ICanvasOptions options)
    : IAssignments
{
    /// <summary>
    /// The identifier of the associated course.
    /// </summary>
    public CourseIdentifier? CourseIdentifier { get; set; }

    /// <summary>
    /// Starts a new <see cref="Assignment"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Assignment"/> instance.</returns>
    public Assignment New()
    {
        return new Assignment(parent)
        {
            CourseId = CourseIdentifier ?? Entities.CourseIdentifier.None,
            Id = AssignmentIdentifier.None,
        };
    }

    /// <summary>
    /// Retrieves an assignment.
    /// </summary>
    /// <param name="identifier">The identifier of the assignment.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Assignment"/> instance if found; <see langword="null"/> otherwise.</returns>
    public async Task<Assignment?> Retrieve(AssignmentIdentifier identifier, CancellationToken cancellationToken)
    {
        logger.LogDebug("Retrieving assignment with id {id}", identifier);
        var dto = await connection.GetEntity<AssignmentDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/courses/{CourseIdentifier}/assignments/{identifier}"),
                options,
                cancellationToken)
            .ConfigureAwait(false);
        return Assignment.From(parent, dto);
    }
}