using System.Net;
using System.Net.Http.Json;

using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Implementations;
using Canvas.Client.Tests.Helpers;

namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Unit tests for <see cref="StandardAssignmentGroupsClient"/>.
/// </summary>
public class StandardAssignmentGroupsClientTests
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
            .AssignmentGroups(identifier)
            .New();

        // Assert
        result.ShouldSatisfyAllConditions(
            r => r.ShouldNotBeNull(),
            r => r.Client.ShouldBe(client),
            r => r.CourseId.ShouldBe(identifier),
            r => r.Id.ShouldBe(AssignmentGroupIdentifier.None));
    }

    [Fact]
    public async Task RetrieveRetrievesAnAssignmentGroup()
    {
        // Arrange
        const string courseId = "1234";
        const string groupId = "5678";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new AssignmentDto { Id = groupId })
        };
        factory.MessageHandler.AddResponse($"/api/v1/courses/{courseId}/assignment_groups/{groupId}", response);
        var client = factory.Get();
        var courseIdentifier = CourseIdentifier.From(courseId);
        var groupIdentifier = AssignmentGroupIdentifier.From(groupId);

        // Act
        var result = await client
            .Courses
            .AssignmentGroups(courseIdentifier)
            .Retrieve(groupIdentifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(groupIdentifier),
            r => r.CourseId.ShouldBe(courseIdentifier));
        factory.MessageHandler.ValidateCalls();
    }

    [Fact]
    public async Task RetrieveHandlesMissingAssignment()
    {
        // Arrange
        const string courseId = "1234";
        const string groupId = "5678";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        factory.MessageHandler.AddResponse($"/api/v1/courses/{courseId}/assignment_groups/{groupId}", response);
        var client = factory.Get();
        var courseIdentifier = CourseIdentifier.From(courseId);
        var groupIdentifier = AssignmentGroupIdentifier.From(groupId);

        // Act
        var result = await client
            .Courses
            .AssignmentGroups(courseIdentifier)
            .Retrieve(groupIdentifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeNull();
        factory.MessageHandler.ValidateCalls();
    }
}
