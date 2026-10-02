namespace Canvas.Client.Entities;
/// <summary>
/// The User entity definition.
/// </summary>
public sealed partial record User
{
    private UserDto? _source;
    private readonly ICanvasClient _client;
    internal User(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the User is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// Optional If avatars are enabled and caller is admin, this field can be requested and will contain the current state of the user&apos;s avatar.
    /// </summary> 
    /// <example>
    /// &quot;approved&quot;
    /// </example>
    public string? AvatarState { get; init; }
    /// <summary>
    /// If avatars are enabled, this field will be included and contain a url to retrieve the user&apos;s avatar.
    /// </summary> 
    /// <example>
    /// &quot;https://en.gravatar.com/avatar/d8cb8c8cd40ddf0cd05241443a591868?s=80&amp;r=g&quot;
    /// </example>
    public string? AvatarUrl { get; init; }
    /// <summary>
    /// Optional: The user&apos;s bio.
    /// </summary> 
    /// <example>
    /// &quot;I like the Muppets.&quot;
    /// </example>
    public string? Bio { get; init; }
    /// <summary>
    /// Optional: This field can be requested with certain API calls, and will return the users primary email address.
    /// </summary> 
    /// <example>
    /// &quot;sheldon@caltech.example.com&quot;
    /// </example>
    public string? Email { get; init; }
    /// <summary>
    /// Optional: This field can be requested with certain API calls, and will return a list of the users active enrollments. See the List enrollments API for more details about the format of these records.
    /// </summary>
    public IList<Enrollment>? Enrollments { get; init; }
    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string? FirstName { get; init; }
    /// <summary>
    /// The ID of the user.
    /// </summary> 
    /// <example>
    /// 2
    /// </example>
    public UserIdentifier Id { get; init; }
    /// <summary>
    /// The integration_id associated with the user.  This field is only included if the user came from a SIS import and has permissions to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;ABC59802&quot;
    /// </example>
    public string? IntegrationId { get; init; }
    /// <summary>
    /// Optional: This field is only returned in certain API calls, and will return a timestamp representing the last time the user logged in to canvas.
    /// </summary> 
    /// <example>
    /// 2012-05-30T17:45:25.0000000+00:00
    /// </example>
    public DateTime? LastLogin { get; init; }
    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string? LastName { get; init; }
    /// <summary>
    /// Optional: This field can be requested with certain API calls, and will return the users locale in RFC 5646 format.
    /// </summary> 
    /// <example>
    /// &quot;tlh&quot;
    /// </example>
    public string? Locale { get; init; }
    /// <summary>
    /// The unique login id for the user.  This is what the user uses to log in to Canvas.
    /// </summary> 
    /// <example>
    /// &quot;sheldon@caltech.example.com&quot;
    /// </example>
    public string? LoginId { get; init; }
    /// <summary>
    /// The name of the user.
    /// </summary> 
    /// <example>
    /// &quot;Sheldon Cooper&quot;
    /// </example>
    public string? Name { get; init; }
    /// <summary>
    /// A short name the user has selected, for use in conversations or other less formal places through the site.
    /// </summary> 
    /// <example>
    /// &quot;Shelly&quot;
    /// </example>
    public string? ShortName { get; init; }
    /// <summary>
    /// A list of the sections this user is part of in the courses they are part of.
    /// </summary> 
    /// <example>
    /// &quot;Section 1, Section 2, Section 2a&quot;
    /// </example>
    public string? Sections { get; init; }
    /// <summary>
    /// The id of the SIS import.  This field is only included if the user came from a SIS import and has permissions to manage SIS information.
    /// </summary> 
    /// <example>
    /// 18
    /// </example>
    public  string ? SisImportId { get; init; }
    /// <summary>
    /// The SIS ID associated with the user.  This field is only included if the user came from a SIS import and has permissions to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? SisUserId { get; init; }
    /// <summary>
    /// The name of the user that is should be used for sorting groups of users, such as in the gradebook.
    /// </summary> 
    /// <example>
    /// &quot;Cooper, Sheldon&quot;
    /// </example>
    public string? SortableName { get; init; }
    /// <summary>
    /// Optional: This field is only returned in certain API calls, and will return the IANA time zone name of the user&apos;s preferred timezone.
    /// </summary> 
    /// <example>
    /// &quot;America/Denver&quot;
    /// </example>
    public string? TimeZone { get; init; }

    internal static User? From(ICanvasClient client, UserDto? dto)
    {
        if (dto == null)
            return null;
        return new User(client)
        {
            AvatarState = dto.AvatarState,
            AvatarUrl = dto.AvatarUrl,
            Bio = dto.Bio,
            Email = dto.Email,
            Enrollments = Enrollment.From(client, dto.Enrollments),
            FirstName = dto.FirstName,
            Id = dto.Id == null ? UserIdentifier.None : UserIdentifier.From(dto.Id),
            IntegrationId = dto.IntegrationId,
            LastLogin = dto.LastLogin,
            LastName = dto.LastName,
            Locale = dto.Locale,
            LoginId = dto.LoginId,
            Name = dto.Name,
            ShortName = dto.ShortName,
            Sections = dto.Sections,
            SisImportId = dto.SisImportId,
            SisUserId = dto.SisUserId,
            SortableName = dto.SortableName,
            TimeZone = dto.TimeZone,
            _source = dto,
        };
    }

    internal static IList<User> From(ICanvasClient client, IEnumerable<UserDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}