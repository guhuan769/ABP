using Serilog;
using Serilog.Events;

namespace MicroClassroom.PublicApp.Host;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
.MinimumLevel.Debug()
#else
        .MinimumLevel.Information()
#endif
.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
.Enrich.FromLogContext()
.WriteTo.Async(c => c.File("Logs/logs.txt"))
.WriteTo.Async(c => c.Console())
.CreateLogger();

        try
        {
            Log.Information("Starting Website.Host.");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();

            await builder.AddApplicationAsync<PublicAppHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Website.Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
