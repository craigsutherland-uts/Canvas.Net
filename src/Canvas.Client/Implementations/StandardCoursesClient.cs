using Canvas.Client.Entities;

using Microsoft.Extensions.Logging;

using System.Globalization;
using System.Runtime.CompilerServices;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="ICourses"/>.
/// </summary>
/// <param name="parent">The parent <see cref="ICanvasClient"/>.</param>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
[CanvasClient<ICourses>]
public sealed class StandardCoursesClient(
        ICanvasClient parent,
        ICanvasConnection connection,
        ILogger<StandardCoursesClient> logger)
    : ICourses
{
    /// <summary>
    /// Lists the courses for the user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Course}"/> containing the courses.</returns>
    public IAsyncEnumerable<Course?> List(
        CancellationToken cancellationToken)
    {
        return List(CanvasOptions.New(), cancellationToken);
    }
    /// <summary>
    /// Lists the courses for the user.
    /// </summary>
    /// <param name="options">The options for the request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Course}"/> containing the courses.</returns>
    public async IAsyncEnumerable<Course?> List(
        CanvasOptions options,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        logger.LogDebug("Listing courses for user");
        await foreach (var dto in connection.ListEntities<CourseDto>(
                "/api/v1/courses",
                options,
                cancellationToken)
            .ConfigureAwait(false))
        {
            var entity = ParseDto(dto);
            if (entity != null) yield return entity;
        }
    }

    /// <summary>
    /// Starts a new <see cref="Course"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Course"/> instance.</returns>
    public Course New()
    {
        return new Course(parent)
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
                CanvasOptions.New(),
                cancellationToken)
            .ConfigureAwait(false);
        return ParseDto(dto);
    }

    /// <summary>
    /// Parses an <see cref="CourseDto"/> into an <see cref="Course"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="CourseDto"/> to parse.</param>
    /// <returns>The parsed <see cref="Course"/> entity, or <see langword="null"/> if the DTO is <see langword="null"/>.</returns>
    private Course? ParseDto(CourseDto? dto)
    {
        var entity = Course.From(parent, dto);
        return entity;
    }
}