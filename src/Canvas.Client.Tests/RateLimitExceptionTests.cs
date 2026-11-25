namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="RateLimitException"/>.
/// </summary>
public class RateLimitExceptionTests
{
    [Fact]
    public void FullConstructor()
    {
        // Arrange
        var inner = new Exception();
        const string url = "http://somewhere";

        // Act
        var ex = new RateLimitException(url, inner);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(Errors.RateLimitException),
            e => e.InnerException.ShouldBe(inner));
    }

    [Fact]
    public void MessageOnlyConstructor()
    {
        // Arrange
        const string url = "http://nowhere";

        // Act
        var ex = new RateLimitException(url);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(Errors.RateLimitException),
            e => e.InnerException.ShouldBeNull());
    }
}
