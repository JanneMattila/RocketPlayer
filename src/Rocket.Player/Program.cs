using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Rocket.Player
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var environmentName = "Production";
            if (Debugger.IsAttached)
            {
                environmentName = "Development";
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var options = configuration.Get<GamePlayerOptions>();
            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    var loggingConfiguration = configuration.GetSection("Logging");
                    loggingBuilder.AddConfiguration(loggingConfiguration);
                    loggingBuilder.AddFile(loggingConfiguration);
                    loggingBuilder.AddConsole();
                })
                .AddApplicationInsightsTelemetry()
                .AddSingleton(options)
                .AddSingleton<GamePlayer>()
                .BuildServiceProvider();

            // Start delay (in case of local development)
            await Task.Delay(configuration.GetValue<int>("StartupDelay"));

            var player = serviceProvider.GetService<GamePlayer>();
            await player.Play();
        }
    }
}
