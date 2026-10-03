using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level assignment interactions.
/// </summary>
public interface IAssignments
{
    /// <summary>
    /// The identifier of the associated course.
    /// </summary>
    CourseIdentifier? CourseIdentifier { get; set; }

    /// <summary>
    /// Starts a new <see cref="Assignment"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Assignment"/> instance.</returns>
    Assignment New();

    /// <summary>
    /// Retrieves an assignment.
    /// </summary>
    /// <param name="identifier">The identifier of the assignment.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Assignment"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<Assignment?> Retrieve(AssignmentIdentifier identifier, CancellationToken cancellationToken);
}
