namespace Canvas.Client.Entities;
/// <summary>
/// The Assignment group entity definition.
/// </summary>
public sealed partial record AssignmentGroup
{
    private AssignmentGroupDto? _source;
    private readonly ICanvasClient _client;
    internal AssignmentGroup(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the AssignmentGroup is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// the assignments in this Assignment Group (see the Assignment API for a detailed list of fields)
    /// </summary> 
    /// <example>
    /// []
    /// </example>
    public IList<int>? Assignments { get; init; }
    /// <summary>
    /// the weight of the Assignment Group
    /// </summary> 
    /// <example>
    /// 20
    /// </example>
    public int? GroupWeight { get; init; }
    /// <summary>
    /// the id of the Assignment Group
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public AssignmentGroupIdentifier Id { get; init; }
    /// <summary>
    /// the integration data of the Assignment Group
    /// </summary>
    public object? IntegrationData { get; init; }
    /// <summary>
    /// the name of the Assignment Group
    /// </summary> 
    /// <example>
    /// &quot;group2&quot;
    /// </example>
    public string? Name { get; init; }
    /// <summary>
    /// the position of the Assignment Group
    /// </summary> 
    /// <example>
    /// 7
    /// </example>
    public int? Position { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public GradingRules? Rules { get; init; }
    /// <summary>
    /// the sis source id of the Assignment Group
    /// </summary> 
    /// <example>
    /// &quot;1234&quot;
    /// </example>
    public string? SisSourceId { get; init; }

    internal static AssignmentGroup? From(ICanvasClient client, AssignmentGroupDto? dto)
    {
        if (dto == null)
            return null;
        return new AssignmentGroup(client)
        {
            Assignments = dto.Assignments,
            GroupWeight = dto.GroupWeight,
            Id = dto.Id == null ? AssignmentGroupIdentifier.None : AssignmentGroupIdentifier.From(dto.Id),
            IntegrationData = dto.IntegrationData,
            Name = dto.Name,
            Position = dto.Position,
            Rules = GradingRules.From(client, dto.Rules),
            SisSourceId = dto.SisSourceId,
            _source = dto,
        };
    }

    internal static IList<AssignmentGroup> From(ICanvasClient client, IEnumerable<AssignmentGroupDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}