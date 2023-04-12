using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Factories;

public class ConfigFactory
{
    private readonly IConfigurationRoot _config;
    private readonly string? _firstConnectionString;

    public ConfigFactory ()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        //load all known config values
        _firstConnectionString = _config.GetRequiredSection("ConnectionStrings").GetChildren().First().Get<string>();
    }

    public void LogConfigDebugView()
    {
        DataAccessLayer.ServiceProvider.GetRequiredService<LoggerFactory>().Log.LogDebug("Config debug view:\n{DebugView}", _config.GetDebugView());
    }

    public string GetFirstConnectionString()
    {
        if (_firstConnectionString != null)
        {
            return _firstConnectionString;
        }

        throw new InvalidOperationException("Connection string not found!");
    }
}