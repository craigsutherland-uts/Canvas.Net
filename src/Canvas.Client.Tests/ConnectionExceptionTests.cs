namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="ConnectionException"/>.
/// </summary>
public class ConnectionExceptionTests
{
    [Fact]
    public void FullConstructor()
    {
        // Arrange
        var inner = new Exception();
        const string msg = "full-exception";
        const string url = "http://somewhere";

        // Act
        var ex = new ConnectionException(url, msg, inner);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(msg),
            e => e.InnerException.ShouldBe(inner));
    }

    [Fact]
    public void MessageOnlyConstructor()
    {
        // Arrange
        const string msg = "message-only-exception";
        const string url = "http://nowhere";

        // Act
        var ex = new ConnectionException(url, msg);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(msg),
            e => e.InnerException.ShouldBeNull());
    }
}
