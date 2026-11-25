using System.Net;
using System.Net.Http.Json;

using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Implementations;
using Canvas.Client.Tests.Helpers;

namespace Canvas.Client.Tests.Implementations;

/// <summary>
/// Unit tests for <see cref="StandardAccountsClient"/>.
/// </summary>
public class StandardAccountsClientTests
{
    [Fact]
    public void NewRetrievesANewEntity()
    {
        // Arrange
        var factory = new TestClientFactory();
        var client = factory.Get();

        // Act
        var result = client
            .Accounts
            .New();

        // Assert
        result.ShouldSatisfyAllConditions(
            r => r.ShouldNotBeNull(),
            r => r.Client.ShouldBe(client),
            r => r.Id.ShouldBe(AccountIdentifier.None));
    }

    [Fact]
    public async Task RetrieveRetrievesAnAccount()
    {
        // Arrange
        const string id = "1234";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new AccountDto { Id = id })
        };
        factory.MessageHandler.AddResponse($"/api/v1/accounts/{id}", response);
        var client = factory.Get();
        var identifier = AccountIdentifier.From(id);

        // Act
        var result = await client
            .Accounts
            .Retrieve(identifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldSatisfyAllConditions(
            r => r.Id.ShouldBe(identifier));
        factory.MessageHandler.ValidateCalls();
    }

    [Fact]
    public async Task RetrieveHandlesMissingAccount()
    {
        // Arrange
        const string id = "12345";
        var factory = new TestClientFactory();
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        factory.MessageHandler.AddResponse($"/api/v1/accounts/{id}", response);
        var client = factory.Get();
        var identifier = AccountIdentifier.From(id);

        // Act
        var result = await client
            .Accounts
            .Retrieve(identifier, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeNull();
        factory.MessageHandler.ValidateCalls();
    }
}
