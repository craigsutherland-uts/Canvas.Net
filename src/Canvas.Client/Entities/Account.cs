using Canvas.Client.Dtos;
using Canvas.Client.Interfaces;
using Canvas.Client.Internal;
using CommunityToolkit.Diagnostics;

namespace Canvas.Client.Entities;
/// <summary>
/// The Account entity definition.
/// </summary>
public sealed partial record Account
{
    private AccountDto? _source;
    private readonly IAccounts _client;
    internal Account(IAccounts client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the Account is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// The storage quota for a group in the account in megabytes, if not otherwise specified
    /// </summary> 
    /// <example>
    /// 50
    /// </example>
    public int? DefaultGroupStorageQuotaMb { get; init; }
    /// <summary>
    /// The storage quota for the account in megabytes, if not otherwise specified
    /// </summary> 
    /// <example>
    /// 500
    /// </example>
    public int? DefaultStorageQuotaMb { get; init; }
    /// <summary>
    /// The default time zone of the account. Allowed time zones are {http://www.iana.org/time-zones IANA time zones} or friendlier {http://api.rubyonrails.org/classes/ActiveSupport/TimeZone.html Ruby on Rails time zones}.
    /// </summary> 
    /// <example>
    /// "America/Denver"
    /// </example>
    public string? DefaultTimeZone { get; init; }
    /// <summary>
    /// The storage quota for a user in the account in megabytes, if not otherwise specified
    /// </summary> 
    /// <example>
    /// 50
    /// </example>
    public int? DefaultUserStorageQuotaMb { get; init; }
    /// <summary>
    /// the ID of the Account object
    /// </summary> 
    /// <example>
    /// 2
    /// </example>
    public AccountIdentifier Id { get; init; }
    /// <summary>
    /// The account's identifier in the Student Information System. Only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// "123xyz"
    /// </example>
    public string? IntegrationId { get; init; }
    /// <summary>
    /// The account's identifier that is sent as context_id in LTI launches.
    /// </summary> 
    /// <example>
    /// "123xyz"
    /// </example>
    public string? LtiGuid { get; init; }
    /// <summary>
    /// The display name of the account
    /// </summary> 
    /// <example>
    /// "Canvas Account"
    /// </example>
    public string? Name { get; init; }
    /// <summary>
    /// The account's parent ID, or null if this is the root account
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public AccountIdentifier? ParentAccountId { get; init; }
    /// <summary>
    /// The ID of the root account, or null if this is the root account
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public AccountIdentifier? RootAccountId { get; init; }
    /// <summary>
    /// The account's identifier in the Student Information System. Only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// "123xyz"
    /// </example>
    public string? SisAccountId { get; init; }
    /// <summary>
    /// The id of the SIS import if created through SIS. Only included if the user has permission to manage SIS information.
    /// </summary> 
    /// <example>
    /// 12
    /// </example>
    public int? SisImportId { get; init; }
    /// <summary>
    /// The UUID of the account
    /// </summary> 
    /// <example>
    /// "WvAHhY5FINzq5IyRIJybGeiXyFkG3SqHUPb7jZY5"
    /// </example>
    public string? Uuid { get; init; }
    /// <summary>
    /// The state of the account. Can be 'active' or 'deleted'.
    /// </summary> 
    /// <example>
    /// "active"
    /// </example>
    public string? WorkflowState { get; init; }

    internal static Account? From(IAccounts client, AccountDto? dto)
    {
        if (dto == null)
            return null;
        return new Account(client)
        {
            DefaultGroupStorageQuotaMb = dto.DefaultGroupStorageQuotaMb,
            DefaultStorageQuotaMb = dto.DefaultStorageQuotaMb,
            DefaultTimeZone = dto.DefaultTimeZone,
            DefaultUserStorageQuotaMb = dto.DefaultUserStorageQuotaMb,
            Id = dto.Id == null ? AccountIdentifier.None : AccountIdentifier.From(dto.Id),
            IntegrationId = dto.IntegrationId,
            LtiGuid = dto.LtiGuid,
            Name = dto.Name,
            ParentAccountId = AccountIdentifier.FromNullable(dto.ParentAccountId),
            RootAccountId = AccountIdentifier.FromNullable(dto.RootAccountId),
            SisAccountId = dto.SisAccountId,
            SisImportId = dto.SisImportId,
            Uuid = dto.Uuid,
            WorkflowState = dto.WorkflowState,
            _source = dto,
        };
    }
}