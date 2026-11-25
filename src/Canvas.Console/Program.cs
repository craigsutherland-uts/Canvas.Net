using Canvas.Client;
using Canvas.Client.Entities;
using Canvas.Client.Options;
using Canvas.Generator;

using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Extensions.Logging;

namespace Canvas.Console;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        var serilogLogger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
        var loggerFactory = new SerilogLoggerFactory(serilogLogger);
        var rootLogger = loggerFactory.CreateLogger<Program>();

        // Modes:
        // 1 = test Canvas client
        // 2 = generate code
        var mode = 1;

        var tokenSource = new CancellationTokenSource();
        var cancellationToken = tokenSource.Token;
        if (mode == 1)
        {
            await TestCanvasClient(loggerFactory, rootLogger, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            await GenerateClientCode(loggerFactory, rootLogger, cancellationToken).ConfigureAwait(false);
        }

        return 0;
    }

    private static async Task GenerateClientCode(SerilogLoggerFactory loggerFactory, ILogger<Program> rootLogger, CancellationToken cancellationToken)
    {
        rootLogger.LogInformation("Importing definitions");
        var engine = new Engine
        {
            Logger = loggerFactory.CreateLogger<Engine>(),
            
        };
        await engine.Initialise(
                "canvas.yaml",
                "api.yaml",
                cancellationToken)
            .ConfigureAwait(false);

        rootLogger.LogInformation("Analysing components");
        try
        {
            await engine.GenerateEntities(
                    "C:\\Repos\\Canvas.Net\\src\\Canvas.Client",
                    cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            rootLogger.LogError(ex, "Unable to generate entities");
        }
    }

    private static async Task TestCanvasClient(SerilogLoggerFactory loggerFactory, ILogger<Program> rootLogger, CancellationToken cancellationToken)
    {
        var factory = new CanvasClientFactory
        {
            LoggerFactory = loggerFactory,
        };
        var settings = AppSettings.New("appsettings.json");
        rootLogger.LogInformation("The API URL is {url}", settings.CanvasUrl);
        try
        {
            var client = factory.Get(settings.CanvasUrl, settings.CanvasToken);
            await foreach (var course in client.Courses.List(
                CanvasOptions.New().WithInclude(CourseInclude.All).WithPageSize(100).WithMasquerading(UserIdentifier.From(80591)),
                cancellationToken)
            .ConfigureAwait(false))
            {
                rootLogger.LogInformation("Course: {name} [{id}]", course?.Name, course?.Id);
            }

        }
        catch (Exception ex)
        {
            rootLogger.LogError(ex, "Canvas connection test failed!");
        }
    }
}
