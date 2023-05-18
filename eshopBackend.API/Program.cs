using eshopBackend.DAL;
using eshopBackend.DAL.Extensions;

namespace eshopBackend.API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddLogging(b => b
            .AddDebug()
            .AddConsole());

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //health checks (primarily for Docker)
        builder.Services.AddHealthChecks();

        //use user secrets if any
        builder.Configuration.AddUserSecrets<Program>(true);

        builder.Services.RegisterDalDependencies();

        WebApplication app = builder.Build();
        
        //migrate db
        using (IServiceScope scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetService<DbMigrator>()!.Migrate();
        }

        app.MapHealthChecks("/health");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}