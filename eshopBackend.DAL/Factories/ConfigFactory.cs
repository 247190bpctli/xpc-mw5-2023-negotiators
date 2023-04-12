using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Factories;

public class ConfigFactory
{
    private readonly IConfigurationRoot _config;
    private readonly string? _firstConnectionString;
    private readonly LogLevel _defaultLogLevel;
    private readonly ILogger<ConfigFactory> _logger;

    public ConfigFactory (ILogger<ConfigFactory> logger)
    {
        _logger = logger;
        
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        //load all known config values
        _firstConnectionString = _config.GetRequiredSection("ConnectionStrings").GetChildren().First().Get<string>();
        Enum.TryParse(_config.GetRequiredSection("Logging").GetRequiredSection("LogLevel")
            .GetRequiredSection("Default").Get<string>(), out _defaultLogLevel);
    }

    public void LogConfigDebugView()
    {
        _logger.LogDebug("Config debug view:\n{DebugView}", _config.GetDebugView());
    }

    public string GetFirstConnectionString()
    {
        if (_firstConnectionString != null)
        {
            return _firstConnectionString;
        }

        throw new InvalidOperationException("Connection string not found!");
    }
    
    public LogLevel GetDefaultLogLevel()
    {
        return _defaultLogLevel;
    }
}