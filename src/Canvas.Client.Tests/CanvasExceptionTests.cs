using Canvas.Client.Entities;

namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="CanvasException"/>.
/// </summary>
public class CanvasExceptionTests
{
    [Fact]
    public void FullConstructor()
    {
        // Arrange
        var errors = new CanvasError[] { new() };
        var inner = new Exception();
        const string url = "http://somewhere";
        const string msg = "first-message";

        // Act
        var ex = new CanvasException(url, msg, errors, inner);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(msg),
            e => e.Errors.ShouldBe(errors),
            e => e.InnerException.ShouldBe(inner));
    }

    [Fact]
    public void MessageOnlyConstructor()
    {
        // Arrange
        var errors = new CanvasError[] { new() };
        const string url = "http://nowhere";
        const string msg = "second-message";

        // Act
        var ex = new CanvasException(url, msg, errors);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Url.ShouldBe(url),
            e => e.Message.ShouldBe(msg),
            e => e.Errors.ShouldBe(errors),
            e => e.InnerException.ShouldBeNull());
    }
}
