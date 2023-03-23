using Microsoft.Extensions.Configuration;

namespace eshopBackend.DAL;

public class ConfigLoader
{
    private readonly IConfigurationRoot _config;

    private readonly string? _firstConnectionString;
    
    public ConfigLoader ()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        //load all known config values
        _firstConnectionString = _config.GetRequiredSection("ConnectionStrings").GetChildren().First().Get<string>();
    }

    //TODO: LogConfigDebugView
    public void ConfigDebugView()
    {
        Console.WriteLine(_config.GetDebugView());
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