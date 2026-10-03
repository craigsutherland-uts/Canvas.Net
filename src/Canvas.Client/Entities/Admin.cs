namespace Canvas.Client.Entities;
/// <summary>
/// The Admin entity definition.
/// </summary>
public sealed partial record Admin
{
    private AdminDto? _source;
    private readonly ICanvasClient _client;
    internal Admin(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the Admin is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// The unique identifier for the account role/user assignment.
    /// </summary> 
    /// <example>
    /// 1023
    /// </example>
    public int? Id { get; init; }
    /// <summary>
    /// The account role assigned. This can be &apos;AccountAdmin&apos; or a user-defined role created by the Roles API.
    /// </summary> 
    /// <example>
    /// &quot;AccountAdmin&quot;
    /// </example>
    public string? Role { get; init; }
    /// <summary>
    /// A Canvas user, e.g. a student, teacher, administrator, observer, etc.
    /// </summary>
    public User? User { get; init; }
    /// <summary>
    /// The status of the account role/user assignment.
    /// </summary> 
    /// <example>
    /// &quot;deleted&quot;
    /// </example>
    public string? WorkflowState { get; init; }

    internal static Admin? From(ICanvasClient client, AdminDto? dto)
    {
        if (dto == null)
            return null;
        return new Admin(client)
        {
            Id = dto.Id,
            Role = dto.Role,
            User = Entities.User.From(client, dto.User),
            WorkflowState = dto.WorkflowState,
            _source = dto,
        };
    }

    internal static IList<Admin> From(ICanvasClient client, IEnumerable<AdminDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}