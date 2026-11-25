namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="UnauthorizedException"/>.
/// </summary>
public class UnauthorizedExceptionTests
{
    [Fact]
    public void FullConstructor()
    {
        // Arrange
        var inner = new Exception();
        const string url = "http://somewhere";

        // Act
        var ex = new UnauthorizedException(url, inner);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(Errors.UnauthorizedException),
            e => e.InnerException.ShouldBe(inner));
    }

    [Fact]
    public void MessageOnlyConstructor()
    {
        // Arrange
        const string url = "http://nowhere";

        // Act
        var ex = new UnauthorizedException(url);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(Errors.UnauthorizedException),
            e => e.InnerException.ShouldBeNull());
    }
}
