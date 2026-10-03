using System.Diagnostics;

using Canvas.Client.Implementations;

using Microsoft.Extensions.DependencyInjection;

namespace Canvas.Client.Entities;

[DebuggerDisplay($"{{{nameof(Name)}}} [{{{nameof(Id)}.ToString()}}]")]
public partial record Course
{
    private Lazy<IAssignments> _assignments = new();

    partial void Initialise()
    {
        _assignments = new(() =>
        {
            var client = ((StandardClient)_client).Services.GetRequiredService<IAssignments>();
            client.CourseIdentifier = Id;
            return client;
        });
    }

    /// <summary>
    /// The assignments for this course.
    /// </summary>
    public IAssignments Assignments => _assignments.Value;

    /// <summary>
    /// Refreshes the details from Canvas.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A new <see cref="Course"/> instance with the refreshed details.</returns>
    /// <remarks>
    /// This method will return a new instance of the <see cref="Course"/>, it does not modify
    /// the existing instance.
    /// </remarks>
    public async Task<Course> Refresh(CancellationToken cancellationToken)
    {
        var refreshed = await _client
                .Courses
                .Retrieve(Id, cancellationToken)
                .ConfigureAwait(false)
            ?? throw new CanvasClientException("Unable to refresh course: Canvas returned a not found result");
        return refreshed;
    }
}
