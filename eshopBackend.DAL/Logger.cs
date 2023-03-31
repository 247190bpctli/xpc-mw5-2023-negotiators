using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL;

public class Logger
{
    public ILogger Log;
    private readonly ConfigLoader _config;

    public Logger(ConfigLoader config)
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

        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder);
        Log = loggerFactory.CreateLogger<Logger>();
    }
}