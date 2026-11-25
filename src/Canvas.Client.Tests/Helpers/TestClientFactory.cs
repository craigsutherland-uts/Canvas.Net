using Canvas.Client.Implementations;
using Canvas.Client.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Canvas.Client.Tests.Helpers;

/// <summary>
/// A version of <see cref="CanvasClientFactory"/> for generating test-based clients.
/// </summary>
public sealed class TestClientFactory
{
    private readonly Lazy<List<(Type, Type)>> _canvasClients = new(ScanForCanvasClients);

    public FakeHttpMessageHandler MessageHandler { get; } = new();

    public ICanvasClient Get()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        // Add the default options
        services
            .AddTransient(sp => new HttpClient(MessageHandler))
            .AddSingleton<ICanvasConnection>(sp => new HttpConnection(
                FakeHttpMessageHandler.DefaultRootUrl,
                "1234",
                sp.GetRequiredService<HttpClient>(),
                sp.GetRequiredService<ILogger<HttpConnection>>()))
            .AddSingleton<IInternalCanvasClient, StandardClient>();

        // Add the registered clients
        foreach (var (service, implementation) in _canvasClients.Value)
        {
            services.AddTransient(service, implementation);
        }

        // Generate the root client and return it
        var client = services
            .BuildServiceProvider()
            .GetRequiredService<IInternalCanvasClient>();
        return client;
    }

    /// <remarks>
    /// Copied from ..\Canvas.Client\CanvasClientFactory.cs
    /// </remarks>
    private static List<(Type, Type)> ScanForCanvasClients()
    {
        var clients = new List<(Type, Type)>();
        foreach (var type in typeof(CanvasClientFactory).Assembly.GetTypes())
        {
            foreach (var attrib in type.GetCustomAttributes(inherit: false))
            {
                var aType = attrib.GetType();
                if (!aType.IsGenericType || aType.GetGenericTypeDefinition() != typeof(CanvasClientAttribute<>))
                {
                    continue;
                }

                clients.Add((aType.GetGenericArguments()[0], type));
            }
        }

        return clients;
    }

}