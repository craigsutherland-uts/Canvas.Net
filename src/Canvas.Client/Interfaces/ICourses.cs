using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level course interactions.
/// </summary>
public interface ICourses
{
    /// <summary>
    /// Retrieves an <see cref="IAssignmentGroups"/> for a course.
    /// </summary>
    /// <param name="courseIdentifier">The identifier of the course.</param>
    /// <returns>The <see cref="IAssignmentGroups"/> instance.</returns>
    IAssignmentGroups AssignmentGroups(CourseIdentifier courseIdentifier);

    /// <summary>
    /// Retrieves an <see cref="IAssignments"/> for a course.
    /// </summary>
    /// <param name="courseIdentifier">The identifier of the course.</param>
    /// <returns>The <see cref="IAssignments"/> instance.</returns>
    IAssignments Assignments(CourseIdentifier courseIdentifier);

    /// <summary>
    /// Lists the courses for the user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Course}"/> containing the courses.</returns>
    IAsyncEnumerable<Course?> List(CancellationToken cancellationToken);

    /// <summary>
    /// Lists the courses for the user.
    /// </summary>
    /// <param name="options">The options for the request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Course}"/> containing the courses.</returns>
    IAsyncEnumerable<Course?> List(
        CanvasOptions options,
        CancellationToken cancellationToken);

    /// <summary>
    /// Starts a new <see cref="Course"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Course"/> instance.</returns>
    Course New();

    /// <summary>
    /// Retrieves a course.
    /// </summary>
    /// <param name="identifier">The identifier of the course.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Course"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<Course?> Retrieve(CourseIdentifier identifier, CancellationToken cancellationToken);
}
