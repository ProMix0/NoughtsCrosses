using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NoughtsCrosses.Classes;
using NoughtsCrosses.DI;
using NoughtsCrosses.WpfClient;

Console.WriteLine("Building host");

IHost host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(config =>
        config.AddJsonFile("Settings.json"))
    .ConfigureServices((context, services) =>
        services
            .AddNoughtsCrossesGame(builder =>
                    builder
                        .AddPlayer<AiPlayer>()
                        .AddPlayer<WpfPlayer>()
                        .AddField<OptimizedField>()
                        .AddGame<Game<AiPlayer, WpfPlayer>>(),
                true)
    )
    .UseConsoleLifetime()
    .Build();

Console.WriteLine("Running host");

await host.RunAsync();

Console.WriteLine("Host shut down");