using Microsoft.Extensions.Logging;

namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="CanvasClientFactory"/>.
/// </summary>
public class CanvasClientFactoryTests
{
    [Fact]
    public void GetInitialisesANewClient()
    {
        // Arrange
        var factory = new CanvasClientFactory();

        // Act
        var client = factory.Get("https://somewhere", "1234");

        // Assert
        client.ShouldSatisfyAllConditions(
            c => c.ShouldNotBeNull(),
            c => c.Accounts.ShouldNotBeNull());
    }

    [Fact]
    public void GetWithLoggerFactoryInitialisesANewClientAndAddsLogger()
    {
        // Arrange
        var factory = new CanvasClientFactory
        {
            LoggerFactory = A.Fake<ILoggerFactory>(),
        };

        // Act
        var client = factory.Get("https://somewhere", "1234");

        // Assert
        client.ShouldSatisfyAllConditions(
            c => c.ShouldNotBeNull(),
            c => c.Accounts.ShouldNotBeNull());
    }
}