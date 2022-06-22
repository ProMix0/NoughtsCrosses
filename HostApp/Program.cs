using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Classes;
using NoughtsCrosses.Services;
using NoughtsCrosses.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfClient;
using NoughtsCrosses.DI;

Console.WriteLine("Building host");

IHost host = Host.CreateDefaultBuilder()

    .ConfigureAppConfiguration(config =>
        config.AddJsonFile("Settings.json"))

    .ConfigureServices((context, services) =>
        services

        .AddNoughtsCrossesGame(builder =>
            builder
                .UsePlayers<AiPlayer, WpfPlayer>()
                .UseField<OptimizedField>()
                .UseGame<Game>())
        )

    .UseConsoleLifetime()

    .Build();

Console.WriteLine("Running host");

await host.RunAsync();

Console.WriteLine("Host shut down");