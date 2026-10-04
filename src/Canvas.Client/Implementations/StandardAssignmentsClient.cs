using Canvas.Client.Entities;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="IAssignments"/>.
/// </summary>
/// <param name="parent">The parent <see cref="ICanvasClient"/>.</param>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
[CanvasClient<IAssignments>]
public sealed class StandardAssignmentsClient(
        ICanvasClient parent,
        ICanvasConnection connection,
        ILogger<StandardAssignmentsClient> logger)
    : IAssignments
{
    /// <summary>
    /// The identifier of the associated course.
    /// </summary>
    public CourseIdentifier? CourseIdentifier { get; set; }

    /// <summary>
    /// Lists the assignments for the associated course.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Assignment}"/> containing the assignments.</returns>
    public async IAsyncEnumerable<Assignment?> List(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        logger.LogDebug("Listing assignments from course {courseId}", CourseIdentifier);
        await foreach (var dto in connection.ListEntities<AssignmentDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/courses/{CourseIdentifier}/assignments"),
                CanvasOptions.New(),
                cancellationToken)
            .ConfigureAwait(false))
        {
            var entity = ParseDto(dto);
            if (entity != null) yield return entity;
        }
    }

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
        logger.LogDebug("Retrieving assignment with id {id} from course {courseId}", identifier, CourseIdentifier);
        var dto = await connection.GetEntity<AssignmentDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/courses/{CourseIdentifier}/assignments/{identifier}"),
                CanvasOptions.New(),
                cancellationToken)
            .ConfigureAwait(false);
        return ParseDto(dto);
    }

    /// <summary>
    /// Parses an <see cref="AssignmentDto"/> into an <see cref="Assignment"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="AssignmentDto"/> to parse.</param>
    /// <returns>The parsed <see cref="Assignment"/> entity, or <see langword="null"/> if the DTO is <see langword="null"/>.</returns>
    private Assignment? ParseDto(AssignmentDto? dto)
    {
        var entity = Assignment.From(parent, dto);
        if (entity != null && entity.CourseId == Entities.CourseIdentifier.None)
        {
            entity = entity with
            {
                CourseId = CourseIdentifier ?? Entities.CourseIdentifier.None,
            };
        }

        return entity;
    }
}