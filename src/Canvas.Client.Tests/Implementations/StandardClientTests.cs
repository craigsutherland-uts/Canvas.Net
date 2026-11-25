namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Unit tests for <see cref="StandardClient"/>.
/// </summary>
public class StandardClientTests(StandardClientFixture standardClientFixture)
        : IClassFixture<StandardClientFixture>
{
    [Fact]
    public void AccountsRetrievesClient()
    {
        // Act
        var client = standardClientFixture.Client;

        // Assert
        client.Accounts.ShouldNotBeNull();
    }

    [Fact]
    public void CoursesRetrievesClient()
    {
        // Act
        var client = standardClientFixture.Client;

        // Assert
        client.Courses.ShouldNotBeNull();
    }

    [Fact]
    public void UsersRetrievesClient()
    {
        // Act
        var client = standardClientFixture.Client;

        // Assert
        client.Users.ShouldNotBeNull();
    }
}
