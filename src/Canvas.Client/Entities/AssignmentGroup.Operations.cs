namespace Canvas.Client.Entities;

public partial record AssignmentGroup
{
    /// <summary>
    /// The ID of the course that owns the assignment group.
    /// </summary> 
    /// <example>
    /// 123
    /// </example>
    public CourseIdentifier CourseId { get; init; }
}
