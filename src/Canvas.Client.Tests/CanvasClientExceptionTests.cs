namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="CanvasClientException"/>.
/// </summary>
public class CanvasClientExceptionTests
{
    [Fact]
    public void FullConstructor()
    {
        // Arrange
        var inner = new Exception();
        const string msg = "full-exception";

        // Act
        var ex = new CanvasClientException(msg, inner);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Message.ShouldBe(msg),
            e => e.InnerException.ShouldBe(inner));
    }

    [Fact]
    public void MessageOnlyConstructor()
    {
        // Arrange
        const string msg = "message-only-exception";

        // Act
        var ex = new CanvasClientException(msg);

        // Assert
        ex.ShouldSatisfyAllConditions(
            e => e.Message.ShouldBe(msg),
            e => e.InnerException.ShouldBeNull());
    }
}
