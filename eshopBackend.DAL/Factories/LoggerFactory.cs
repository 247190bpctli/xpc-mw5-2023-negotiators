using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Factories;

public class LoggerFactory
{
    public readonly ILogger Log;
    private readonly ConfigFactory _config;

    public LoggerFactory(ConfigFactory config)
    {
        _config = config;
        
        Action<ILoggingBuilder> builder = builder =>
        {
            //TODO: take levels from API -> appsettings.json
            #if DEBUG
                builder.AddConsole().SetMinimumLevel(LogLevel.Trace);
            #else
                builder.AddConsole().SetMinimumLevel(LogLevel.Warning);
            #endif
        };

        using ILoggerFactory loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder);
        Log = loggerFactory.CreateLogger<LoggerFactory>();
    }
}