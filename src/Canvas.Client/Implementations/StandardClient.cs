using Canvas.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="ICanvasClient"/>.
/// </summary>
/// <remarks>
/// Initialise a new <see cref="StandardClient"/> instance.
/// </remarks>
/// <param name="services">The underlying <see cref="IServiceProvider"/> instance.</param>
public sealed class StandardClient(IServiceProvider services)
        : ICanvasClient
{
    private readonly Lazy<IAccounts> _accounts = new(services.GetRequiredService<IAccounts>);

    /// <summary>
    /// The root-level account functionality.
    /// </summary>
    public IAccounts Accounts => _accounts.Value;
}
