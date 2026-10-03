using Canvas.Client.Implementations;
using Canvas.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Canvas.Client;

/// <summary>
/// A factory for generating <see cref="ICanvasClient"/> instances.
/// </summary>
public sealed class CanvasClientFactory
{
    // Cache all the defined clients.
    private readonly Lazy<List<(Type, Type)>> _canvasClients = new(ScanForCanvasClients);

    /// <summary>
    /// A logger factory to use in any clients.
    /// </summary>
    public ILoggerFactory? LoggerFactory { get; set; }

    /// <summary>
    /// Retrieves an <see cref="ICanvasClient"/> instance.
    /// </summary>
    /// <param name="apiUrl">The URL to the Canvas instance.</param>
    /// <param name="apiKey">The Canvas API key.</param>
    /// <returns>A <see cref="ICanvasClient"/> instance.</returns>
    public ICanvasClient Get(string apiUrl, string apiKey)
    {
        var services = new ServiceCollection();

        // Add logging
        if (LoggerFactory != null)
        {
            services
                .AddSingleton(LoggerFactory)
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }
        else
        {
            services.AddLogging();
        }

        // Add the default options
        services
            .AddHttpClient()
            .AddSingleton<ICanvasOptions, StandardCanvasOptions>()
            .AddSingleton<ICanvasConnection>(sp => new HttpConnection(
                apiUrl,
                apiKey,
                sp.GetRequiredService<HttpClient>(),
                sp.GetRequiredService<ILogger<HttpConnection>>()))
            .AddSingleton<ICanvasClient, StandardClient>();

        // Add the registered clients
        foreach (var (service, implementation) in _canvasClients.Value)
        {
            services.AddTransient(service, implementation);
        }

        // Generate the root client and return it
        var client = services
            .BuildServiceProvider()
            .GetRequiredService<ICanvasClient>();
        return client;
    }

    /// <summary>
    /// Scans the current assembly for all the registered clients.
    /// </summary>
    /// <returns>A list of Canvas client types with their implementations.</returns>
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