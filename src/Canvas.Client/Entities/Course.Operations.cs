using System.Diagnostics;

using Canvas.Client.Implementations;

using Microsoft.Extensions.DependencyInjection;

namespace Canvas.Client.Entities;

[DebuggerDisplay($"{{{nameof(Name)}}} [{{{nameof(Id)}.ToString()}}]")]
public partial record Course
{
#pragma warning disable IDE0044 // Add readonly modifier
    private Lazy<IAssignments> _assignments = null!;
    private Lazy<IAssignmentGroups> _assignmentGroups = null!;
#pragma warning restore IDE0044 // Add readonly modifier

    partial void Initialise()
    {
        _assignments = new(() =>
        {
            var client = _client.Services.GetRequiredService<IAssignments>();
            ((StandardAssignmentsClient)client).CourseIdentifier = Id;
            return client;
        });
        _assignmentGroups = new(() =>
        {
            var client = _client.Services.GetRequiredService<IAssignmentGroups>();
            client.CourseIdentifier = Id;
            return client;
        });
    }

    /// <summary>
    /// The assignments for this course.
    /// </summary>
    public IAssignments Assignments => _assignments.Value;

    /// <summary>
    /// The assignment groups for this course.
    /// </summary>
    public IAssignmentGroups AssignmentGroups => _assignmentGroups.Value;

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
            ?? throw new CanvasClientException(
                string.Format(Errors.UnableToRefreshEntityError, "course")); 
        return refreshed;
    }
}
