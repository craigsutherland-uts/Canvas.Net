using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Canvas.Console;

internal class AppSettings
{
    public static AppSettings New(string configPath)
    {
        var settings = new AppSettings();
        settings.Initialise(configPath);
        return settings;
    }

    private IConfiguration? _config;

    private bool _isInitialized;

    public string CanvasToken => GetSetting("canvas:token");

    public string CanvasUrl => GetSetting("canvas:url");

    public string GetSetting(string path)
    {
        if (!_isInitialized) throw new InvalidOperationException("Cannot get a setting before the settings have been initialized");

        var section = _config!.GetSection(path);
        return (section.Exists()
            ? section.Value
            : string.Empty) ?? string.Empty;
    }

    public void Initialise(string configPath)
    {
        Guard.IsNotNullOrWhiteSpace(configPath);
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile(configPath);
        builder.AddUserSecrets<AppSettings>();
        _config = builder.Build();
        _isInitialized = true;
    }
}