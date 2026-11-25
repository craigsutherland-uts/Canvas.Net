using System.Net;
using System.Net.Http.Json;

using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Tests.Helpers;

namespace Canvas.Client.Tests.Entities;

/// <summary>
/// Unit tests for <see cref="Course"/>.
/// </summary>
public class CourseTests
{
    [Fact]
    public async Task RefreshThrowsAnError()
    {
        // Arrange
        const string id = "1234";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        factory.MessageHandler.AddResponse($"/api/v1/courses/{id}", response);
        var client = factory.Get();
        var identifier = CourseIdentifier.From(id);
        var entity = new Course(client) { Id = identifier };

        // Act
        var ex = await Assert.ThrowsAsync<CanvasClientException>(
            async () => await entity.Refresh(TestContext.Current.CancellationToken));

        // Assert
        ex.Message.ShouldBe("Unable to refresh course: Canvas returned a not found result");
    }

    [Fact]
    public async Task RefreshReturnsNewEntity()
    {
        // Arrange
        const string id = "1234";
        const string name = "new-name";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new CourseDto { Id = id, Name = name })
        };
        factory.MessageHandler.AddResponse($"/api/v1/courses/{id}", response);
        var client = factory.Get();
        var identifier = CourseIdentifier.From(id);
        var entity = new Course(client) { Id = identifier };

        // Act
        var result = await entity.Refresh(TestContext.Current.CancellationToken);

        // Assert
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(identifier),
            r => r.Name.ShouldBe(name),
            r => r.IsNew.ShouldBeFalse());
    }
}
