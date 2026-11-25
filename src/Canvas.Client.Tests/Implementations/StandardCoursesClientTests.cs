using System.Net;
using System.Net.Http.Json;

using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Implementations;
using Canvas.Client.Tests.Helpers;

using Microsoft.Extensions.Logging;

namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Unit tests for <see cref="StandardCoursesClient"/>.
/// </summary>
public class StandardCoursesClientTests
{
    [Fact]
    public void NewRetrievesANewEntity()
    {
        // Arrange
        var client = A.Fake<ICanvasClient>();
        var connection = A.Fake<ICanvasConnection>();
        var logger = A.Fake<ILogger<StandardCoursesClient>>();
        var coursesClient = new StandardCoursesClient(
            client,
            connection,
            logger);

        // Act
        var result = coursesClient.New();

        // Assert
        result.ShouldSatisfyAllConditions(
            r => r.ShouldNotBeNull(),
            r => r.Client.ShouldBe(client),
            r => r.Id.ShouldBe(CourseIdentifier.None));
    }

    [Fact]
    public async Task RetrieveRetrievesACourse()
    {
        // Arrange
        const string id = "1234";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new CourseDto { Id = id })
        };
        factory.MessageHandler.AddResponse($"/api/v1/courses/{id}", response);
        var client = factory.Get();
        var identifier = CourseIdentifier.From(id);

        // Act
        var result = await client
            .Courses
            .Retrieve(identifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(identifier));
        factory.MessageHandler.ValidateCalls();
    }

    [Fact]
    public async Task RetrieveHandlesMissingCourse()
    {
        // Arrange
        const string id = "12345";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        factory.MessageHandler.AddResponse($"/api/v1/courses/{id}", response);
        var client = factory.Get();
        var identifier = CourseIdentifier.From(id);

        // Act
        var result = await client
            .Courses
            .Retrieve(identifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeNull();
        factory.MessageHandler.ValidateCalls();
    }
}
