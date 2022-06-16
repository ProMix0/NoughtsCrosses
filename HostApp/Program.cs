using CrossesZeroes.Abstractions;
using CrossesZeroes.Classes;
using CrossesZeroes.Services;
using CrossesZeroes.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfClient;
using CrossesZeroes.DI;

Console.WriteLine("Building host");

IHost host = Host.CreateDefaultBuilder()

    .ConfigureAppConfiguration(config =>
        config.AddJsonFile("Settings.json"))

    .ConfigureServices((context, services) =>
        services

        .AddCrossesZeroesGame(builder=>
            builder
                .UsePlayers<AiPlayer,WpfPlayer>()
                .UseField<ExtraCustomizableField>()
                .UseGame<CrossesZeroesGame>())
        )

    .UseConsoleLifetime()

    .Build();

Console.WriteLine("Running host");

await host.RunAsync();

Console.WriteLine("Host shut down");