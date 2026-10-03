using System.Globalization;
using System.Runtime.CompilerServices;

using Canvas.Client.Entities;

using Microsoft.Extensions.Logging;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="IAssignmentGroups"/>.
/// </summary>
/// <param name="parent">The parent <see cref="ICanvasClient"/>.</param>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
/// <param name="options">The <see cref="ICanvasOptions"/> to use when connecting to Canvas.</param>
[CanvasClient<IAssignmentGroups>]
public sealed class StandardAssignmentGroupsClient(
        ICanvasClient parent,
        ICanvasConnection connection,
        ILogger<StandardAssignmentGroupsClient> logger,
        ICanvasOptions options)
    : IAssignmentGroups
{
    /// <summary>
    /// The identifier of the associated course.
    /// </summary>
    public CourseIdentifier? CourseIdentifier { get; set; }

    /// <summary>
    /// Lists the assignment groups for the associated course.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{AssignmentGroup}"/> containing the assignment groups.</returns>
    public async IAsyncEnumerable<AssignmentGroup?> List(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        logger.LogDebug("Listing assignment groups from course {courseId}", CourseIdentifier);
        await foreach (var dto in connection.ListEntities<AssignmentGroupDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/courses/{CourseIdentifier}/assignment_groups"),
                options,
                cancellationToken)
            .ConfigureAwait(false))
        {
            var entity = ParseDto(dto);
            if (entity != null) yield return entity;
        }
    }

    /// <summary>
    /// Starts a new <see cref="AssignmentGroup"/> instance.
    /// </summary>
    /// <returns>A new <see cref="AssignmentGroup"/> instance.</returns>
    public AssignmentGroup New()
    {
        return new AssignmentGroup(parent)
        {
            CourseId = CourseIdentifier ?? Entities.CourseIdentifier.None,
            Id = AssignmentGroupIdentifier.None,
        };
    }

    /// <summary>
    /// Retrieves an assignment group.
    /// </summary>
    /// <param name="identifier">The identifier of the assignment group.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="AssignmentGroup"/> instance if found; <see langword="null"/> otherwise.</returns>
    public async Task<AssignmentGroup?> Retrieve(AssignmentGroupIdentifier identifier, CancellationToken cancellationToken)
    {
        logger.LogDebug("Retrieving assignment group with id {id}", identifier);
        var dto = await connection.GetEntity<AssignmentGroupDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/courses/{CourseIdentifier}/assignment_groups/{identifier}"),
                options,
                cancellationToken)
            .ConfigureAwait(false);
        return ParseDto(dto);
    }

    /// <summary>
    /// Parses an <see cref="AssignmentGroupDto"/> into an <see cref="AssignmentGroup"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="AssignmentGroupDto"/> to parse.</param>
    /// <returns>The parsed <see cref="AssignmentGroup"/> entity, or <see langword="null"/> if the DTO is <see langword="null"/>.</returns>
    private AssignmentGroup? ParseDto(AssignmentGroupDto? dto)
    {
        var entity = AssignmentGroup.From(parent, dto);
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