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
            .WriteTo.Console()
            .CreateLogger();
        var loggerFactory = new SerilogLoggerFactory(serilogLogger);
        var rootLogger = loggerFactory.CreateLogger<Program>();

        rootLogger.LogInformation("Importing definitions");
        var tokenSource = new CancellationTokenSource();
        var cancellationToken = tokenSource.Token;
        var engine = new Engine
        {
            Logger = loggerFactory.CreateLogger<Engine>(),
        };
        await engine.Initialise(
                "canvas.yaml",
                cancellationToken)
            .ConfigureAwait(false);

        rootLogger.LogInformation("Analysing components");
        try
        {
            await engine.GenerateEntities(
                    "Entities",
                    cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            rootLogger.LogError(ex, "Unable to generate entities");
        }

        return 0;
    }
}
