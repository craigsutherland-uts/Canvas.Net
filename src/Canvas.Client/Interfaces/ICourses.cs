using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level course interactions.
/// </summary>
public interface ICourses
{
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
