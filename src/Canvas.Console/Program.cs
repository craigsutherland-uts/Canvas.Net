using Canvas.Client;
using Canvas.Client.Entities;
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
        var mode = 1;

        var tokenSource = new CancellationTokenSource();
        var cancellationToken = tokenSource.Token;
        if (mode == 1)
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
                var accountId = AccountIdentifier.Self;
                var root = await client.Accounts.Retrieve(
                        accountId,
                        cancellationToken)
                    .ConfigureAwait(false);
                if (root == null)
                {
                    rootLogger.LogWarning("Unable to retrieve account {id}", accountId);
                }
                else
                {
                    rootLogger.LogInformation("The account name is {name} [{id}]", root.Name, root.Id);
                }
            }
            catch (Exception ex)
            {
                rootLogger.LogError(ex, "Canvas connection test failed!");
            }
        }
        else
        {
            rootLogger.LogInformation("Importing definitions");
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
        }

        return 0;
    }
}
