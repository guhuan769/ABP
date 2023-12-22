using Elon.Forum.DbMigrator;
using Elon.Forum.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
        .MinimumLevel.Override("Elon.Forum", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Zhaoxi.Forum", LogEventLevel.Information)
#endif
        .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("logs/logs.log"))
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

await CreateHostBuilder(args).RunConsoleAsync();


static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
   .AddAppSettingsSecretsJson()
   .ConfigureLogging((context, logging) => logging.ClearProviders())
   .ConfigureServices((hostContext, services) =>
   {
       var configuration = hostContext.Configuration;
       var connectionString = configuration.GetConnectionString("Default");

       services.AddDbContext<ForumDbContext>(
           options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
           action =>
           {
               action.MigrationsAssembly("Elon.Forum.DbMigrator");
           }));

       services.AddHostedService<DbMigratorHostedService>();
   });
