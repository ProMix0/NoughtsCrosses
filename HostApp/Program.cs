using CrossesZeroes.Abstractions;
using CrossesZeroes.Classes;
using CrossesZeroes.Services;
using CrossesZeroes.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfClient;

Console.WriteLine("Building host");

IHost host = Host.CreateDefaultBuilder()
    .ConfigureHostOptions(options =>
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost)

    .ConfigureAppConfiguration(config =>
        config.AddJsonFile("Settings.json"))

    .ConfigureServices((context, services) =>
        services

        .AddOptions<CustomizableField.Configuration>(builder =>
            builder
            .BindConfiguration(CustomizableField.Configuration.SectionName)
            .Validate(CustomizableField.Configuration.Validate))

        .Configure<AiPlayer.AiPlayerBehaviour>(behaviour => behaviour.wantRepeat = true)

        .AddTransient<CrossesZeroesAbstract, CrossesZeroesWithAi>()
        .AddTransient<IRealPlayer, WpfPlayer>()
        .AddTransient<IAiPlayer, AiPlayer>()
        .AddTransient<ICrossesZeroesField, ExtraCustomizableField>()

        .AddHostedService<CrossesZeroesLoopService>()
        )

    .UseConsoleLifetime()

    .Build();

Console.WriteLine("Running host");

await host.RunAsync();

Console.WriteLine("Host shut down");