using System.Net;
using System.Net.Http.Json;

using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Implementations;
using Canvas.Client.Tests.Helpers;

namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Unit tests for <see cref="StandardAssignmentsClient"/>.
/// </summary>
public class StandardAssignmentsClientTests
{
    [Fact]
    public void NewRetrievesANewEntity()
    {
        // Arrange
        var factory = new TestClientFactory();
        var client = factory.Get();
        var identifier = CourseIdentifier.From("987654");

        // Act
        var result = client
            .Courses
            .Assignments(identifier)
            .New();

        // Assert
        result.ShouldSatisfyAllConditions(
            r => r.ShouldNotBeNull(),
            r => r.Client.ShouldBe(client),
            r => r.CourseId.ShouldBe(identifier),
            r => r.Id.ShouldBe(AssignmentIdentifier.None));
    }

    [Fact]
    public async Task RetrieveRetrievesAnAssignment()
    {
        // Arrange
        const string courseId = "1234";
        const string assignmentId = "5678";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new AssignmentDto { Id = assignmentId })
        };
        factory.MessageHandler.AddResponse($"/api/v1/courses/{courseId}/assignments/{assignmentId}", response);
        var client = factory.Get();
        var courseIdentifier = CourseIdentifier.From(courseId);
        var assignmentIdentifier = AssignmentIdentifier.From(assignmentId);

        // Act
        var result = await client
            .Courses
            .Assignments(courseIdentifier)
            .Retrieve(assignmentIdentifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(assignmentIdentifier),
            r => r.CourseId.ShouldBe(courseIdentifier));
        factory.MessageHandler.ValidateCalls();
    }

    [Fact]
    public async Task RetrieveHandlesMissingAssignment()
    {
        // Arrange
        const string courseId = "1234";
        const string assignmentId = "5678";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        factory.MessageHandler.AddResponse($"/api/v1/courses/{courseId}/assignments/{assignmentId}", response);
        var client = factory.Get();
        var courseIdentifier = CourseIdentifier.From(courseId);
        var assignmentIdentifier = AssignmentIdentifier.From(assignmentId);

        // Act
        var result = await client
            .Courses
            .Assignments(courseIdentifier)
            .Retrieve(assignmentIdentifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeNull();
        factory.MessageHandler.ValidateCalls();
    }
}
