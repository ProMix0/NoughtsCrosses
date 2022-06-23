using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Classes;
using NoughtsCrosses.Services;
using NoughtsCrosses.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoughtsCrosses.WpfClient;
using NoughtsCrosses.DI;

Console.WriteLine("Building host");

IHost host = Host.CreateDefaultBuilder()

    .ConfigureAppConfiguration(config =>
        config.AddJsonFile("Settings.json"))

    .ConfigureServices((context, services) =>
        services

        .AddNoughtsCrossesGame(builder =>
            builder
                .UsePlayer<AiPlayer>()
                .UsePlayer<WpfPlayer>()
                .UseField<OptimizedField>()
                .UseGame<Game<AiPlayer, WpfPlayer>>(),
            true)
        )

    .UseConsoleLifetime()

    .Build();

Console.WriteLine("Running host");

await host.RunAsync();

Console.WriteLine("Host shut down");