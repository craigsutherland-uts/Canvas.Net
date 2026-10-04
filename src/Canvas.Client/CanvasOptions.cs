using System.Globalization;

using Canvas.Client.Entities;

using Humanizer;

namespace Canvas.Client;

/// <summary>
/// The options for a request to the Canvas API.
/// </summary>
public sealed class CanvasOptions
{
    private readonly List<Parameter> _parameters;

    private CanvasOptions()
    {
        _parameters = [];
    }

    private CanvasOptions(CanvasOptions original, params Parameter[] parameters)
    {
        _parameters = new List<Parameter>(original._parameters.Count + parameters.Length);
        _parameters.AddRange(original._parameters);
        _parameters.AddRange(parameters);
    }

    /// <summary>
    /// Generates a new set of options.
    /// </summary>
    /// <returns>A new <see cref="Options"/> instance.</returns>
    public static CanvasOptions New() => new();

    /// <summary>
    /// Generates a new set of options with paging parameters.
    /// </summary>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A new <see cref="Options"/> instance with the paging parameters added.</returns>
    public CanvasOptions WithPageSize(int pageSize) => new(
        this,
        new Parameter("per_page", pageSize.ToString(CultureInfo.InvariantCulture)));

    /// <summary>
    /// Generates a new set of options with masquerading parameters.
    /// </summary>
    /// <param name="user">The user to masquerade as.</param>
    /// <returns>A new <see cref="Options"/> instance with the masquerading parameters added.</returns>
    public CanvasOptions WithMasquerading(User user) => new(
        this,
        new Parameter("as_user_id", user.Id.ToString(CultureInfo.InvariantCulture)));

    /// <summary>
    /// Generates a new set of options with masquerading parameters.
    /// </summary>
    /// <param name="userIdentifier">The identifier of the user to masquerade as.</param>
    /// <returns>A new <see cref="Options"/> instance with the masquerading parameters added.</returns>
    public CanvasOptions WithMasquerading(UserIdentifier userIdentifier) => new(
        this,
        new Parameter("as_user_id", userIdentifier.ToString(CultureInfo.InvariantCulture)));

    /// <summary>
    /// Generates a new set of options with include parameters.
    /// </summary>
    /// <typeparam name="TEnum">The type of include option.</typeparam>
    /// <param name="values">The include options.</param>
    /// <returns>A new <see cref="Options"/> instance with the include parameters added.</returns>
    public CanvasOptions WithInclude<TEnum>(params TEnum[] values)
        where TEnum : struct, Enum
    {
        var isFlags = Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute));
        if (!isFlags)
        {
            return new CanvasOptions(
                this,
                [.. values.Select(v => new Parameter("include[]", v.ToString().Underscore()))]);
        }

        var parameters = new List<Parameter>();
        var flags = Enum
            .GetValues<TEnum>()
            .Where(f => !f.Equals(default(TEnum)))
            .ToArray();
        foreach (var value in values)
        {
            if (value.Equals(default(TEnum))) continue;

            foreach (var flag in flags)
            {
                if (value.HasFlag(flag))
                {
                    parameters.Add(new Parameter("include[]", flag.ToString().Underscore()));
                }
            }
        }

        return new CanvasOptions(this, [.. parameters]);
    }

    /// <summary>
    /// Converts the options to a query string.
    /// </summary>
    /// <returns>The query string.</returns>
    public string ToQueryString()
    {
        return _parameters.Count == 0
            ? string.Empty
            : "?" + string.Join(
                '&',
                _parameters.Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.Value)}"));
    }
}
