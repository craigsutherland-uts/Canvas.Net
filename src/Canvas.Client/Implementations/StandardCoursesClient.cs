using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="ICourses"/>.
/// </summary>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
/// <param name="options">The <see cref="ICanvasOptions"/> to use when connecting to Canvas.</param>
[CanvasClient<ICourses>]
public sealed class StandardCoursesClient(
        ICanvasConnection connection,
        ILogger<StandardCoursesClient> logger,
        ICanvasOptions options)
    : ICourses
{
    /// <summary>
    /// Starts a new <see cref="Course"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Course"/> instance.</returns>
    public Course New()
    {
        return new Course(this)
        {
            Id = CourseIdentifier.None,
        };
    }

    /// <summary>
    /// Retrieves a course.
    /// </summary>
    /// <param name="identifier">The identifier of the course.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Course"/> instance if found; <see langword="null"/> otherwise.</returns>
    public async Task<Course?> Retrieve(CourseIdentifier identifier, CancellationToken cancellationToken)
    {
        logger.LogDebug("Retrieving course with id {id}", identifier);
        var dto = await connection.GetEntity<CourseDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/courses/{identifier}"),
                options,
                cancellationToken)
            .ConfigureAwait(false);
        return Course.From(this, dto);
    }
}