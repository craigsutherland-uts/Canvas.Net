using System.Net;
using System.Net.Http.Json;

using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Implementations;
using Canvas.Client.Tests.Helpers;

using Microsoft.Extensions.Logging;

namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Unit tests for <see cref="StandardUsersClient"/>.
/// </summary>
public class StandardUsersClientTests
{
    [Fact]
    public void NewRetrievesANewEntity()
    {
        // Arrange
        var client = A.Fake<ICanvasClient>();
        var connection = A.Fake<ICanvasConnection>();
        var logger = A.Fake<ILogger<StandardUsersClient>>();
        var usersClient = new StandardUsersClient(
            client,
            connection,
            logger);

        // Act
        var result = usersClient.New();

        // Assert
        result.ShouldSatisfyAllConditions(
            r => r.ShouldNotBeNull(),
            r => r.Client.ShouldBe(client),
            r => r.Id.ShouldBe(UserIdentifier.None));
    }

    [Fact]
    public async Task RetrieveRetrievesAUser()
    {
        // Arrange
        const string id = "1234";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new UserDto { Id = id })
        };
        factory.MessageHandler.AddResponse($"/api/v1/users/{id}", response);
        var client = factory.Get();
        var identifier = UserIdentifier.From(id);

        // Act
        var result = await client
            .Users
            .Retrieve(identifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(identifier));
        factory.MessageHandler.ValidateCalls();
    }

    [Fact]
    public async Task RetrieveSelfRetrievesAUser()
    {
        // Arrange
        const string id = "123456";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new UserDto { Id = id })
        };
        factory.MessageHandler.AddResponse("/api/v1/users/self", response);
        var client = factory.Get();
        var identifier = UserIdentifier.From(id);

        // Act
        var result = await client
            .Users
            .RetrieveSelf(TestContext.Current.CancellationToken);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(identifier));
        factory.MessageHandler.ValidateCalls();
    }

    [Fact]
    public async Task RetrieveHandlesMissingUser()
    {
        // Arrange
        const string id = "12345";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        factory.MessageHandler.AddResponse($"/api/v1/users/{id}", response);
        var client = factory.Get();
        var identifier = UserIdentifier.From(id);

        // Act
        var result = await client
            .Users
            .Retrieve(identifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeNull();
        factory.MessageHandler.ValidateCalls();
    }
}
