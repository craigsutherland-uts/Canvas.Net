namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Fixture for initialising a shared <see cref="ICanvasClient"/>.
/// </summary>
public sealed class StandardClientFixture
{
    private readonly ICanvasClient _client = new CanvasClientFactory().Get("https://testing.com", "123-4");

    public ICanvasClient Client => _client;
}