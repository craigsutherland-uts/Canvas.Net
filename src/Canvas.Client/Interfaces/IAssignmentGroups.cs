using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level assignment group interactions.
/// </summary>
public interface IAssignmentGroups
{
    /// <summary>
    /// The identifier of the associated course.
    /// </summary>
    CourseIdentifier? CourseIdentifier { get; set; }

    /// <summary>
    /// Lists the assignment groups for the associated course.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{AssignmentGroup}"/> containing the assignment groups.</returns>
    IAsyncEnumerable<AssignmentGroup?> List(CancellationToken cancellationToken);

    /// <summary>
    /// Starts a new <see cref="AssignmentGroup"/> instance.
    /// </summary>
    /// <returns>A new <see cref="AssignmentGroup"/> instance.</returns>
    AssignmentGroup New();

    /// <summary>
    /// Retrieves an assignment group.
    /// </summary>
    /// <param name="identifier">The identifier of the assignment group.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="AssignmentGroup"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<AssignmentGroup?> Retrieve(AssignmentGroupIdentifier identifier, CancellationToken cancellationToken);
}
